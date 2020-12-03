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
    public class SiteUsersModel : PageModel
    {
        private IRegisterRepository _registerRepository;
        public SiteUsersModel(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
        public List<RegisterModel> MyUsers { get; set; }
        public void OnGet()
        {

        }
        public PartialViewResult OnGetUsersPartial()
        {
            MyUsers = _registerRepository.GetList();
            return Partial("_UsersList", MyUsers);
        }
    }
}