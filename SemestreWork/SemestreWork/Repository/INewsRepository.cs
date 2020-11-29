using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SemestreWork.Repository
{
    public interface INewsRepository
    {
        int Add(NewsPost product);

        List<NewsPost> GetList();

        NewsPost GetNews(int id);

        int EditNews(NewsPost news);

        int DeleteNews(int id);
    }
}
