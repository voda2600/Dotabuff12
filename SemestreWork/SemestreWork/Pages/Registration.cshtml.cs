using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemestreWork.Models;
using SemestreWork.Repository;

namespace SemestreWork.Pages
{
    public class RegistrationModel : PageModel
    {
        IRegisterRepository _usersRepository;
        public RegistrationModel(IRegisterRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        [BindProperty]
        public List<RegisterModel> usersList { get; set; }

        [BindProperty]
        public RegisterModel user { get; set; }
        
        public IActionResult OnPost()
        {
            user.Role = "Default";
            user.Password = Crypto.HashPassword(user.Password);
            if (ModelState.IsValid)
            {
                var count = _usersRepository.AddUser(user);
                if (count > 0)
                {
                    return RedirectToPage("/About");
                }
            }
            return Page();

        }
    }
}