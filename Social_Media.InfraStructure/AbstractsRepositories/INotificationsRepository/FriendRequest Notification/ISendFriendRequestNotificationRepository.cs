using Social_Media.Data.Models.Notifications.FriendRequestNotifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository
{
    public interface ISendFriendRequestNotificationRepository : IRepository<SendFriendRequestNotification>
    {
        Task<SendFriendRequestNotification> GetByNotificationId(int NotificationId);
    }
}
