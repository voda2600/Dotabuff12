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
        INewsRepository _Repository;
        ICommentsRepository _commentsRepository;    
        public NewsModel(INewsRepository Repository,ICommentsRepository commentsRepository)
        {
            _Repository = Repository;
            _commentsRepository = commentsRepository;
        }

        [BindProperty]
        public Comments comment { get; set; }

        public List<Comments> postsComments { get; set; }

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
            if (postsComments == null)
            {
                postsComments = new List<Comments>();
            }
            postsComments = _commentsRepository.GetList(-id);
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

        public IActionResult OnPostSendComment(int id)
        {
            var a = HttpContext.Session.Get<RegisterModel>("AuthUser");
            comment.CreatorId = a.Id;
            comment.CreatorName = a.Nick;
            comment.PostId = -id;
            if (ModelState.IsValid)
            {
                var count = _commentsRepository.Add(comment);
                if (count > 0)
                {
                    return Redirect("/News/"+id);
                }
            }

            return Redirect("/News/" + id);
        }
        public IActionResult OnPostDeleteComment(int id, int ComId)
        {
            if (ComId > 0)
            {
                var count = _commentsRepository.DeleteComment(ComId);
                if (count > 0)
                {
                    return Redirect("/News/" + id);
                }
            }

            return Redirect("/News/" + id);
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