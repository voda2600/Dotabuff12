using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Models
{
    public class TopPlayer
    {
        public int Id { get; set; }


        public string Name { get; set; }
        
        public DateTime Date { get; set; }
        public string Country { get; set; }

        public string Team { get; set; }
        public string Picture { get; set; }
    }
}
