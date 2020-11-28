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
    public class HomeModel : PageModel
    {
        IRepository _Repository;
        public HomeModel(IRepository Repository)
        {
            _Repository = Repository;
        }

        [BindProperty]
        public List<NewsPost> newsList { get; set; }

        [BindProperty]
        public NewsPost news { get; set; }

        [TempData]
        public string Message { get; set; }
        public void OnGet()
        {
            newsList = _Repository.GetList();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _Repository.DeleteNews(id);
                if (count > 0)
                {
                    Message = "News Deleted Successfully !";
                    return RedirectToPage("/Home");
                }
            }

            return Page();

        }
    }
}