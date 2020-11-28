using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Models
{
    public class NewsPost
    {
        [Key]
        [Display(Name = "Product Id")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Intro { get; set; }

        [Required]
        public string Text { get; set; }


       
    }
}
