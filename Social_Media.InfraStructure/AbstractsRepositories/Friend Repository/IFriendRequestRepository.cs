using Social_Media.Data.Models.Friends;

namespace Social_Media.InfraStructure.AbstractsRepositories.Notifications
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        Task<FriendRequest> GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(string UserId, string FriendUserId);
    }
}
