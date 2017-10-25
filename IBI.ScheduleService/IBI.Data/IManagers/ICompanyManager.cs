using IBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.IManagers
{
    public interface ICompanyManager: IBaseManager<Company>
    {
        Task<Guid> GetIdByTickerAsync(string ticker);
    }
}
