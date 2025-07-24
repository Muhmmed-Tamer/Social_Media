using Social_Media.InfraStructure.AbstractsRepositories;

namespace Social_Media.Services.AbstractsServices
{
    public interface IServices<T> : IRepository<T> where T : class
    {
    }
}
