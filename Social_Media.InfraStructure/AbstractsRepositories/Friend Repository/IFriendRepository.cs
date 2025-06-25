using Social_Media.Data.Models.Friends;
using Social_Media.InfraStructure.AbstractsRepositories;

namespace Social_Media.Repository
{
    public interface IFriendRepository : IRepository<Friend>
    {
        Task<List<string>> GetFriendsIdOFUserByUserIdAsync(string UserId);
        Task BulkInsertAsync(List<Friend> Friends);
    }
}
