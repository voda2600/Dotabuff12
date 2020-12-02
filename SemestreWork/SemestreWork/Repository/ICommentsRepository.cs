using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface ICommentsRepository
    {
        int Add(Comments product);

        List<Comments> GetList(int PostId);

        int DeleteComment(int id);

    }
}
