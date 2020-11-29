using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemestreWork.Models;
using SemestreWork.Repository;

namespace SemestreWork.Pages
{
    public class MetaModel : PageModel
    {
        IMetaRepository metaRepository;
        public MetaModel(IMetaRepository meta)
        {
            metaRepository = meta;
        }
        [BindProperty]
        public MetaPost metaPost { get; set; }

        public string metaTable { get; set; }
        public void OnGet()
        {
            
            metaPost = metaRepository.GetMeta(1);
            metaPost.Text = HttpUtility.HtmlDecode(metaPost.Text);
            metaTable = HttpUtility.HtmlDecode(DotabuffParser("https://ru.dotabuff.com/heroes/winning"));
        }

        public string DotabuffParser(string url)
        {
            string source;
            try { 
            HttpClient client; 
            client = new HttpClient();
            Random random = new Random();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            HttpResponseMessage responce = client.GetAsync(url).Result; 
            source = responce.Content.ReadAsStringAsync().Result;
            var splitMas = source.Split("article");
            source = splitMas[1] ;
            source = source.Remove(0, 1);
            int len = source.Length;
            source = source.Remove(len-2, 2);
            source = source.Replace("sortable", "iksweb");  
            }
            catch
            {
                source = "Статистика пока не доступна!";
            }
            return HttpUtility.HtmlEncode(source);
        }
        
        public IActionResult OnPost()
        {
            var data = metaPost;
            data.Text = HttpUtility.HtmlEncode(metaPost.Text);
            if (ModelState.IsValid)
            {
                var count = metaRepository.EditMeta(data);
                if (count > 0)
                {
                    return Redirect("/Meta");
                }
            }

            return Page();
        }
    }
}