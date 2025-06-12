using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Social_Media.Data.ContextData Data;
        private readonly ILogger<Repository<T>> Logger;
        public Repository(Social_Media.Data.ContextData Data, ILogger<Repository<T>> Logger)
        {
            this.Data = Data;
            this.Logger = Logger;
        }
        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await Data.Set<T>().AddAsync(entity);
            }
            catch
            {
                Logger.LogWarning("");
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                T? Entity = await GetByIdAsync(id);
                if (Entity is not null)
                {
                    Data.Set<T>().Remove(Entity);
                }
            }
            catch
            {
                Logger.LogWarning("");
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Data.Set<T>().AsNoTracking().ToListAsync();
            }
            catch
            {
                Logger.LogWarning("");
                return Enumerable.Empty<T>().ToList();
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await Data.Set<T>().FindAsync(id);
            }
            catch
            {
                Logger.LogWarning("");
                return null;
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await Data.SaveChangesAsync();
            }
            catch
            {
                Logger.LogWarning("");
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                await Task.FromResult(Data.Set<T>().Update(entity));
            }
            catch
            {
                Logger.LogWarning("");
            }
        }
    }
}
