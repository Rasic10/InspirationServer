using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInspiration.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        
        //public virtual ICollection<Song> Songs { get; set; }
    }
}
