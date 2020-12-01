using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface IUserPosstRepository
    {
        int Add(UserPosts product);

        List<UserPosts> GetList(int UserID);

        UserPosts GetPost(int id);

        int EditPost(UserPosts news);

        int DeletePost(int id, int UserId);
    }
}
