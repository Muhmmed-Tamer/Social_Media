using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IRepository<T> Repository;
        private readonly ILogger<Services<T>> Logger;
        public Services(ILogger<Services<T>> Logger, IRepository<T> Repository)
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
                Logger.LogWarning("");
                return Task.CompletedTask;
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
                Logger.LogWarning("");
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
                Logger.LogWarning("");
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
                Logger.LogWarning("");
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
                Logger.LogWarning("");
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
                Logger.LogWarning("");
                return Task.CompletedTask;
            }
        }
    }
}
