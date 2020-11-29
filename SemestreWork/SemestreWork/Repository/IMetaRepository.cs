using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface IMetaRepository
    {
        MetaPost GetMeta(int id);
        int EditMeta(MetaPost product);
    }
}
