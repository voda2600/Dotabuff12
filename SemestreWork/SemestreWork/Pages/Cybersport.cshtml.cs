using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemestreWork.Models;
using SemestreWork.Repository;

namespace SemestreWork.Pages
{
    public class CybersportModel : PageModel
    {
        ICyberKazanRepository _KazanRepository;
        IClobalCyberRepository _GlobalCyber;
        ITopTeamRepository _TopTeamRepository;
        TopPlayersRepository _TopPlayersRepository;
        public CybersportModel(ICyberKazanRepository Repository,IClobalCyberRepository globalCyber, ITopTeamRepository TopTeam, TopPlayersRepository TopPlayersRepository)
        {
            _KazanRepository = Repository;
            _GlobalCyber = globalCyber;
            _TopTeamRepository = TopTeam;
            _TopPlayersRepository = TopPlayersRepository;
        }
        [BindProperty]
        public TopPlayer Topplayer { get; set; }
        [BindProperty]
        public List<TopPlayer> TopPlayersList{ get; set; }
        [BindProperty]
        public TopTeam Topteam { get; set; }
        [BindProperty]
        public List<TopTeam> TopList { get; set; }
        [BindProperty]
        public KazanCyber CyberKazan { get; set; }
        [BindProperty]
        public List<KazanCyber> CyberList { get; set; }
        [BindProperty]
        public GlobalCyber Global { get; set; }
        [BindProperty]
        public List<GlobalCyber> GlobalList{ get; set; }
        public void OnGet()
        {
            CyberList = _KazanRepository.GetList();
            GlobalList = _GlobalCyber.GetList();
            TopList = _TopTeamRepository.GetList();
            TopPlayersList = _TopPlayersRepository.GetList();
        }
        public IActionResult OnPostTop()
        {

            if (ModelState.IsValid)
            {
                var count = _TopTeamRepository.Add(Topteam);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();
        }
        public IActionResult OnPostPlayer()
        {

            if (ModelState.IsValid)
            {
                var count = _TopPlayersRepository.Add(Topplayer);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();
        }
        public IActionResult OnPostKazan()
        {

            if (ModelState.IsValid)
            {
                var count = _KazanRepository.Add(CyberKazan);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();
        }
        public IActionResult OnPostGlobal()
        {

            if (ModelState.IsValid)
            {
                var count = _GlobalCyber.Add(Global);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();
        }
        public IActionResult OnPostDeleteGlobal(int id)
        {
            if (id > 0)
            {
                var count = _GlobalCyber.DeleteCyber(id);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();

        }
        public IActionResult OnPostDeletePlayer(int id)
        {
            if (id > 0)
            {
                var count = _TopPlayersRepository.DeleteCyber(id);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();

        }
        public IActionResult OnPostDeleteTop(int id)
        {
            if (id > 0)
            {
                var count = _TopTeamRepository.DeleteCyber(id);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();

        }
        public IActionResult OnPostDeleteKazan(int id)
        {
            if (id > 0)
            {
                var count = _KazanRepository.DeleteCyber(id);
                if (count > 0)
                {
                    return RedirectToPage("/Cybersport");
                }
            }

            return Page();

        }
    }
}