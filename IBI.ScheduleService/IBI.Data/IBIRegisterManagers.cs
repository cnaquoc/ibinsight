using IBI.Data.IManagers;
using IBI.Data.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data
{
    public static class IBIRegisterManagers
    {
        public static void UseRainWater(this IServiceCollection services)
        {
            services.AddScoped<IStockPriceManager, StockPriceManager>();
        }
    }
}
