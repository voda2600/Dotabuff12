using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface IRegisterRepository
    {
        int AddUser(RegisterModel product);

        List<RegisterModel> GetList();

        RegisterModel GetUser(int id);

        int EditUser(RegisterModel news);

        int DeleteUser(int id);
        int EditUserCookie(RegisterModel user);
        RegisterModel GetAuthUser(string email, string password);
        RegisterModel GetUserByCookie(int CookieId, string Email);
    }
}
