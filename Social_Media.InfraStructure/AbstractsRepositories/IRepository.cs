using Microsoft.EntityFrameworkCore.Storage;

namespace Social_Media.InfraStructure.AbstractsRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransaction();
        Task CommitTransaction(IDbContextTransaction Transaction);
        Task RollbackTransaction(IDbContextTransaction Transaction);
    }
}
