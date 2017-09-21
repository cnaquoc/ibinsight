using IBI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.Data.IManagers
{
    public interface IBaseManager<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllNoTrackingAsync();
        Task<T> GetAsync(Guid id);
        Task<T> SaveAsync(Guid? id, Func<T, T> modifyFunc);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid? id);
        Task DeleteAsync(T entity);
        Task<bool> SaveBatchAsync(ICollection<Guid?> ids, Func<int, T, T> modifyFunc);
    }
}
