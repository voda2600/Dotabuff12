using SemestreWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Repository
{
    public interface ITopTeamRepository
    {
        int Add(TopTeam product);

        List<TopTeam> GetList();

        int DeleteCyber(int id);
    }
}
