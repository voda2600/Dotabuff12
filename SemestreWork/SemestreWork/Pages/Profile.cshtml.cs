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
    public class ProfileModel : PageModel
    {
        IUserPosstRepository _userPostsRepository;
        IRegisterRepository _usersRepository;
        ICommentsRepository _commentsRepository;
        public ProfileModel(IRegisterRepository usersRepository, IUserPosstRepository userPostsRepository, ICommentsRepository commentsRepository)
        {
            _usersRepository = usersRepository;
            _userPostsRepository = userPostsRepository;
            _commentsRepository = commentsRepository;
        }

        [BindProperty]
        public List<UserPosts> userPostsList { get; set; }
        [BindProperty]
        public UserPosts userPost { get; set; }
        [BindProperty]
        public Comments comment { get; set; }

        public List<Comments> postsComments{ get; set; }

        [BindProperty]
        public RegisterModel user { get; set; }

        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            user = _usersRepository.GetUser(id);
            userPostsList = _userPostsRepository.GetList(id);
            userPostsList.Reverse();
            foreach(var a in userPostsList)
            {
                if (postsComments == null)
                {
                    postsComments = new List<Comments>();
                }
                postsComments.AddRange(_commentsRepository.GetList(a.Id));
            }
        }
        public IActionResult OnPost(int id)
        {
            userPost.UserId = id;
            userPost.Time = DateTime.Now;
            if (ModelState.IsValid)
            {
                var count = _userPostsRepository.Add(userPost);
                if (count > 0)
                {
                    return Redirect("/Profile/" + id);
                }
            }

            return Page();
        }
        public IActionResult OnPostSendComment(int id)
        {
            var a = HttpContext.Session.Get<RegisterModel>("AuthUser");
            comment.CreatorId = a.Id;
            comment.CreatorName = a.Nick;
            if (ModelState.IsValid)
            {   
                var count = _commentsRepository.Add(comment);
                if (count > 0)
                {
                    return Redirect("/Profile/" + id);
                }
            }

            return Redirect("/Profile/" + id);
        }
    }
}