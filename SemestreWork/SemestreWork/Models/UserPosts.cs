using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemestreWork.Models
{
    public class UserPosts
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public DateTime Time { get; set; }
     
    }
}
