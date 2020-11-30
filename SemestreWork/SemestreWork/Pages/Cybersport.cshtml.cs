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
        ITopTeamRepository _TopTeam;
        public CybersportModel(ICyberKazanRepository Repository,IClobalCyberRepository globalCyber, ITopTeamRepository TopTeam)
        {
            _KazanRepository = Repository;
            _GlobalCyber = globalCyber;
            _TopTeam = TopTeam;
        }
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
            TopList = _TopTeam.GetList();
        }
        public IActionResult OnPostTop()
        {

            if (ModelState.IsValid)
            {
                var count = _TopTeam.Add(Topteam);
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
        public IActionResult OnPostDeleteTop(int id)
        {
            if (id > 0)
            {
                var count = _TopTeam.DeleteCyber(id);
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