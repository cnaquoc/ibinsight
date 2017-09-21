using IBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.IManagers
{
    public interface IStockPriceManager: IBaseManager<StockPrice>
    {
        Task<ICollection<StockPrice>> GetFullAllAsync();
        Task<StockPrice> GetFullAsync(Guid Id);
    }
}
