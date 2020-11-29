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
    public class MetaModel : PageModel
    {
        IMetaRepository metaRepository;
        public MetaModel(IMetaRepository meta)
        {
            metaRepository = meta;
        }
        [BindProperty]
        public MetaPost metaPost { get; set; }
        public void OnGet()
        {
            metaPost = metaRepository.GetMeta(1);
            metaPost.Text = HttpUtility.HtmlDecode(metaPost.Text);
        }
        public IActionResult OnPost()
        {
            var data = metaPost;
            data.Text = HttpUtility.HtmlEncode(metaPost.Text);
            if (ModelState.IsValid)
            {
                var count = metaRepository.EditMeta(data);
                if (count > 0)
                {
                    return Redirect("/Meta");
                }
            }

            return Page();
        }
    }
}