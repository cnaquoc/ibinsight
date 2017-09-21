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
    class StockPriceManager: BaseManager<StockPrice>, IStockPriceManager
    {
        public StockPriceManager(ApplicationDbContext context) : base(context)
        {
        }

        

        public async Task<ICollection<StockPrice>> GetFullAllAsync()
        {
            //return await context.StockPrices.AsQueryable().Include(e => e.Country)
            //    .OrderBy(e => e.)
            //    .ToListAsync();
            return await context.StockPrices.AsQueryable()                
                .ToListAsync();
        }

        public async Task<StockPrice> GetFullAsync(Guid Id)
        {
            //return await context.StockPrices.AsQueryable().Include(e => e.Country)
            //    .Where(e => e.Id == Id)
            //    .OrderBy(e => e.Name)
            //    .SingleOrDefaultAsync();
            return await context.StockPrices.AsQueryable()
                .Where(e => e.Id == Id)                
                .SingleOrDefaultAsync();
        }
    }
}
