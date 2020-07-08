using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInspiration.Models
{
    public class Fevorite
    {
        public int SongID { get; set; }
        //public virtual Song Song { get; set; }

        public int UserID { get; set; }
        //public virtual User User { get; set; }

       
    }
}
