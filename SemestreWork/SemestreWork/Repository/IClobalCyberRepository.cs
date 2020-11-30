using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface IClobalCyberRepository
    {
        int Add(GlobalCyber product);

        List<GlobalCyber> GetList();

        int DeleteCyber(int id);
    }
}
