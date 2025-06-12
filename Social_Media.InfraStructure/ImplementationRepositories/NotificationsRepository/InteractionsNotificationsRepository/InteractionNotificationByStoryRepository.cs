using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByStoryRepository : Repository<InteractionNotificationByStory>, IInteractionNotificationByStoryRepository
    {
        private readonly DbSet<InteractionNotificationByStory> InteractionNotificationByStory;
        public InteractionNotificationByStoryRepository(Social_Media.Data.ContextData Data) : base(Data)
        {
            this.InteractionNotificationByStory = Data.Set<InteractionNotificationByStory>();
        }
    }
}
