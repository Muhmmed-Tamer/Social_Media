using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository
{
    public interface IInteractionNotificationByStoryRepository : IRepository<InteractionNotificationByStory>
    {
        Task<InteractionNotificationByStory> GetByNotificationId(int NotificationId);
    }
}
