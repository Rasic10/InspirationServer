using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerInspiration.Models
{
    public class DBInitialize
    {
        public static void Initialize(InspirationDBContext context)
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
