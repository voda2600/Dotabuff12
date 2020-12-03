using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SemestreWork.Models;
using SemestreWork.Repository;

namespace SemestreWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)    
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Home", "");
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {

                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = false;
            });
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IMetaRepository, MetaRepository>();
            services.AddTransient<ICyberKazanRepository, CyberKazanRepository>();
            services.AddTransient<IClobalCyberRepository, GlobalCyberRepository>();
            services.AddTransient<ITopTeamRepository, TopTeamRepository>();
            services.AddTransient<TopPlayersRepository>();
            services.AddTransient<IRegisterRepository,RegisterRepository>();
            services.AddTransient<IUserPosstRepository, UserPostsRepository>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRegisterRepository registerRep)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
          
            app.Use(async (context,next) =>
            {
                if (!context.Session.Keys.Contains("AuthReady"))
                    context.Session.Set("AuthReady","false");
                await next.Invoke();
            });
            app.Use(async (context, next) =>
            {
                string CookieId;
                string Email;
                if (context.Request.Cookies.TryGetValue("CookieId", out CookieId)
                &&
                context.Request.Cookies.TryGetValue("Email", out Email))
                {
                    var user = registerRep.GetUserByCookie(int.Parse(CookieId), Email);
                    if (user is null)
                    {
                        await next.Invoke();
                    }
                    else
                    {
                        context.Session.Set<RegisterModel>("AuthUser", user);
                        context.Session.Set("AuthReady", "true");
                    }
                }
    
                await next.Invoke();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
