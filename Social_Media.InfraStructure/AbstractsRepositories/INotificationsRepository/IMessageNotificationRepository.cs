using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.Notifications
{
    public interface IMessageNotificationRepository : IRepository<MessageNotification>
    {
        Task<MessageNotification> GetByNotificationId(int NotificationId);
    }
}
