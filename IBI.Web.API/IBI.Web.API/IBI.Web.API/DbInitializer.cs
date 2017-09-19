using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBI.Data.API;


namespace IBI.Web.API
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Companies.
            if (context.Companies.Any())
            {
                return;   // DB has been seeded
            }
        }
    }
}
