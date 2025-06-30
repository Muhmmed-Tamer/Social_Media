using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IRepository<T> Repository;
        private readonly ILogger Logger;
        public Services(ILogger Logger, IRepository<T> Repository)
        {
            this.Logger = Logger;
            this.Repository = Repository;
        }
        public virtual Task AddAsync(T entity)
        {
            try
            {
                return Repository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.CompletedTask;
            }
        }

        public async Task CommitTransaction(IDbContextTransaction Transaction)
        {
            try
            {
                await Repository.CommitTransaction(Transaction);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        Task<IDbContextTransaction> IRepository<T>.BeginTransaction()
        {
            try
            {
                return Repository.BeginTransaction();
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
                await Repository.RollbackTransaction(Transaction);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public virtual Task DeleteAsync(int id)
        {
            try
            {
                return Repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.CompletedTask;
            }
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return Repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.FromResult<IEnumerable<T>>(Enumerable.Empty<T>());
            }
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            try
            {
                return Repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.FromResult<T>(null);
            }
        }


        public virtual Task SaveChangesAsync()
        {
            try
            {
                return Repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.CompletedTask;
            }
        }

        public virtual Task UpdateAsync(T entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return Task.CompletedTask;
            }
        }

    }
}
