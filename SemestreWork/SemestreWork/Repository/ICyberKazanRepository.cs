using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface ICyberKazanRepository
    {
        int Add(KazanCyber product);

        List<KazanCyber> GetList();

        int EditCyber(KazanCyber news);

        int DeleteCyber(int id);
    }
}
