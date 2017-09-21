using IBI.Data.Entities;
using IBI.Data.IManagers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBI.Data.Helpers;

namespace IBI.Data.Managers
{
    abstract class BaseManager<T> : IBaseManager<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext context;
        protected DbSet<T> entities;
        string errorMessage = string.Empty;

        public BaseManager(ApplicationDbContext context)
        {
            this.context = context;
            if (context != null) entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllNoTrackingAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            T result = await entities.FindAsync(new Object[] { id });
            return result;
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity.Id.IsEmpty()) entity.Id = Guid.NewGuid();
            await entities.AddAsync(entity);
            int x = await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entities.Update(entity);
            int x = await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> SaveAsync(Guid? id, Func<T, T> modifyFunc)
        {
            if (id.IsEmpty()) return await InsertAsync(modifyFunc(Activator.CreateInstance<T>()));
            else return await UpdateAsync(modifyFunc(await GetAsync(id.Value)));
        }

        public async Task DeleteAsync(Guid? id)
        {
            if (id.IsEmpty()) return;

            //T entity = await entities
            //    .AsNoTracking()
            //    .SingleOrDefaultAsync(e=>e.Id == id);

            // Activator.CreateInstance<T>();
            //if (entity != null)
            //{           
            //    entities.Remove(entity);                
            //}

            T entity = Activator.CreateInstance<T>();
            entity.Id = id.Value;
            entities.Remove(entity);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) return;

            entities.Remove(entity);
            int x = await context.SaveChangesAsync();
        }

        protected async Task<bool> SaveBatchAsync(ICollection<Guid?> ids, Func<int, T, T> modifyFunc, Func<T, bool> filterFunc)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                Guid? id = ids.ElementAt(i);
                var entity = modifyFunc(i, (id.IsEmpty()) ? Activator.CreateInstance<T>() : await GetAsync(id.Value));
                if (filterFunc == null || filterFunc(entity))
                {
                    if (id.IsEmpty()) entities.Add(entity);
                    else entities.Update(entity);
                }
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SaveBatchAsync(ICollection<Guid?> ids, Func<int, T, T> modifyFunc)
        {
            return await SaveBatchAsync(ids, modifyFunc, null);
        }

    }
}
