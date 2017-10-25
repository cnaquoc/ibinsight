using IBI.Data.Entities;
using IBI.Data.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IBI.Data.Managers
{
    class CompanyManager: BaseManager<Company>, ICompanyManager
    {
        public CompanyManager(ApplicationDbContext context) : base(context)
        {
        }
        
        public async Task<Guid> GetIdByTickerAsync(string ticker)
        {
            return await context.Companies.AsQueryable().AsNoTracking()
                .Where(e => e.Ticker == ticker.Trim())
                .Select(e => e.Id)
                .SingleOrDefaultAsync();
        }
    }
}
