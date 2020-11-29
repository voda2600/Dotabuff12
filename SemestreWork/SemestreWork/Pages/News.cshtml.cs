using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemestreWork.Models;
using SemestreWork.Repository;


namespace SemestreWork.Pages
{

    public class NewsModel : PageModel
    {
        IRepository _Repository;
        public NewsModel(IRepository Repository)
        {
            _Repository = Repository;
        }

        [BindProperty]
        public List<NewsPost> newsList { get; set; }

        [BindProperty]
        public NewsPost news { get; set; }


        public string HTMLtext { get;  set; }

        public int Id { get; set; }

        public void OnGet(int id)
        {
               Id = id;
               news = _Repository.GetNews(id);
               HTMLtext = HttpUtility.HtmlDecode(news.Text);
               news.Text = HTMLtext;
        }
        public IActionResult OnPost()
        {
            var data = news;
            data.Text = HttpUtility.HtmlEncode(news.Text);
            if (ModelState.IsValid)
            {
                var count = _Repository.EditNews(data);
                if (count > 0)
                {
                    return Redirect("/Home");
                }
            }

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _Repository.DeleteNews(id);
                if (count > 0)
                {
                    return RedirectToPage("/Home");
                }
            }

            return Page();

        }
    }
}