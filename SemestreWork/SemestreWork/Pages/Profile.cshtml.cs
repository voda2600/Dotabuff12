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
        public ProfileModel(IRegisterRepository usersRepository, IUserPosstRepository userPostsRepository)
        {
            _usersRepository = usersRepository;
            _userPostsRepository = userPostsRepository;
        }

        [BindProperty]
        public List<UserPosts> userPostsList { get; set; }
        [BindProperty]
        public UserPosts userPost { get; set; }

        [BindProperty]
        public RegisterModel user { get; set; }

        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            user = _usersRepository.GetUser(id);
            userPostsList = _userPostsRepository.GetList(id);
        }
        public IActionResult OnPost(int id)
        {
            userPost.UserId = id;
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
    }
}