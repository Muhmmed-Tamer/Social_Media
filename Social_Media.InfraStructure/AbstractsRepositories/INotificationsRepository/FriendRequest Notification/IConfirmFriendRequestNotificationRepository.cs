using Social_Media.Data.Models.Notifications.FriendRequestNotifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository
{
    public interface IConfirmFriendRequestNotificationRepository : IRepository<ConfirmFriendRequestNotification>
    {
        Task<ConfirmFriendRequestNotification> GetByNotificationId(int NotificationId);
    }
}
