using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByStoryRepository : Repository<InteractionNotificationByStory>, IInteractionNotificationByStoryRepository
    {
        private readonly DbSet<InteractionNotificationByStory> InteractionNotificationByStory;
        private readonly ILogger<Repository<InteractionNotificationByStory>> Logger;
        public InteractionNotificationByStoryRepository(Social_Media.Data.ContextData Data, ILogger<Repository<InteractionNotificationByStory>> Logger) : base(Data, Logger)
        {
            this.InteractionNotificationByStory = Data.Set<InteractionNotificationByStory>();
        }
    }
}
