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

        IRegisterRepository _usersRepository;
        public ProfileModel(IRegisterRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [BindProperty]
        public RegisterModel user { get; set; }

        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            user = _usersRepository.GetUser(id);
        }
    }
}