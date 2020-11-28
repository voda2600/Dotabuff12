using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface IRepository
    {
        int Add(NewsPost product);

        List<NewsPost> GetList();

        NewsPost GetNews(int id);

        int EditNews(NewsPost product);

        int DeleteNews(int id);
    }
}
