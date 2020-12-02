using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public int PostId { get; set; }
        public string CreatorName { get; set; }

    }
}
