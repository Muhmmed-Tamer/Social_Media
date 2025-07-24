using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Social_Media.InfraStructure.AbstractsRepositories;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Social_Media.Data.ContextData Data;
        private readonly ILogger Logger;
        public Repository(Social_Media.Data.ContextData Data, ILogger Logger)
        {
            this.Data = Data;
            this.Logger = Logger;
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            try
            {
                return await Data.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task CommitTransaction(IDbContextTransaction Transaction)
        {
            try
            {
                await Transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task RollbackTransaction(IDbContextTransaction Transaction)
        {
            try
            {
                await Transaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
        public virtual async Task AddAsync(T entity)
        {
            try
            {
                await Data.Set<T>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
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
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Data.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await Data.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }


        public virtual async Task SaveChangesAsync()
        {
            try
            {
                await Data.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            try
            {
                await Task.FromResult(Data.Set<T>().Update(entity));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
