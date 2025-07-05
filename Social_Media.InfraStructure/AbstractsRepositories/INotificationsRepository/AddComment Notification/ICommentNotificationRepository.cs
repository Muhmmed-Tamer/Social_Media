using Social_Media.Data.Models.Notifications.AddCommentNotification;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository
{
    public interface ICommentNotificationRepository : IRepository<CommentNotification>
    {
        Task BulkInsertAsync(List<CommentNotification> CommentNotifications);
        Task<CommentNotification> GetByNotificationId(int NotificationId);
    }
}
