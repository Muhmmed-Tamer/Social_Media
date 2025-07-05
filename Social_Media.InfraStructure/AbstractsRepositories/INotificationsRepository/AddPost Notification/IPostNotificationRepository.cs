using Social_Media.Data.Models.Notifications.AddPostNotification;

namespace Social_Media.InfraStructure.AbstractsRepositories.Notifications
{
    public interface IPostNotificationRepository : IRepository<PostNotification>
    {
        Task BulkInsertAsync(IEnumerable<PostNotification> PostNotifications);
        Task<PostNotification> GetByNotificationId(int NotificationId);
    }
}
