using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SemestreWork.Models
{
  
    public class NewsPost
    {
     
        public int Id { get; set; }

      
        public string Name { get; set; }

     
        public string Intro { get; set; }

     
        public string Text { get; set; }
        
       
        public string Picture { get; set; }


       
    }
}
