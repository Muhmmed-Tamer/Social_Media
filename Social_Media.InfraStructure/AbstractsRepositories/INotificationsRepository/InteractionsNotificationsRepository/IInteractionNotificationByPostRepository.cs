using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository
{
    public interface IInteractionNotificationByPostRepository : IRepository<InteractionNotificationByPost>
    {
        Task<InteractionNotificationByPost> GetByNotificationId(int NotificationId);
    }
}
