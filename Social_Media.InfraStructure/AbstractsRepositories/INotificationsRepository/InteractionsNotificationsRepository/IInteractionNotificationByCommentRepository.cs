using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository
{
    public interface IInteractionNotificationByCommentRepository : IRepository<InteractionNotificationByComment>
    {
        Task<InteractionNotificationByComment> GetByNotificationId(int NotificationId);
    }
}
