using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemestreWork.Models;
using SemestreWork.Repository;

namespace SemestreWork.Pages
{
    public class AuthorizationModel : PageModel
    {
        IRegisterRepository _usersRepository;
        public AuthorizationModel(IRegisterRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [BindProperty]
        public bool save { get; set; }
        [BindProperty]
        public List<RegisterModel> usersList { get; set; }
        [BindProperty]
        public RegisterModel Anon { get; set; }

        [BindProperty]
        public RegisterModel AuthUser { get; set; }
        [BindProperty]
        public string Message { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            AuthUser = _usersRepository.GetAuthUser(Anon.Email,Crypto.HashPassword(Anon.Password));
            if (AuthUser is null)
            {
                Message = "Не верные данные!";
                return Page();
            }
            else
            {
                HttpContext.Session.Set<RegisterModel>("AuthUser", AuthUser);
                HttpContext.Session.Set("AuthReady", "true");
                HttpContext.Response.Cookies.Append("Email", AuthUser.Email);
                if (save)
                {
                    Random random = new Random();
                    AuthUser.CookieId = random.Next(1, 10000000).ToString();
                    _usersRepository.EditUserCookie(AuthUser);
                    HttpContext.Response.Cookies.Append("CookieId", AuthUser.CookieId);
                }
               
                return Redirect("/Profile/"+AuthUser.Id.ToString());
            }
                
        }
        public IActionResult OnGetLogOut()
        {
            
            var user = HttpContext.Session.Get<RegisterModel>("AuthUser");
            user.CookieId = null;
            _usersRepository.EditUserCookie(user);
            HttpContext.Response.Cookies.Delete("CookieId");
            HttpContext.Session.Clear();
            HttpContext.Session.Set("AuthReady", "false");
            return RedirectToPage("/Home");
        }
    }
}