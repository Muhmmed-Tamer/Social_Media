using Social_Media.Data.Models;

namespace Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository
{
    public interface IUserConnectionRepository : IRepository<UserConnection>
    {
        Task DeleteByConnectionId(string ConnectionId);
        Task DeleteByUserId(string UserId);
        Task<UserConnection?> GetByConnectionId(string ConnectionId);
        Task<List<UserConnection>?> GetByUserId(string UserId);
        Task<List<string>> GetConnectionIdOFFriends(string UserId);
        Task<bool> UserIsOnlineOrNot(string UserId);
    }
}
