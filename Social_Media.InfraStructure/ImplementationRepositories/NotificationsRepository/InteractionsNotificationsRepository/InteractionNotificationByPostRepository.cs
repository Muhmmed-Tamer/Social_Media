using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByPostRepository : Repository<InteractionNotificationByPost>, IInteractionNotificationByPostRepository
    {
        private readonly DbSet<InteractionNotificationByPost> InteractionNotificationByPost;
        public InteractionNotificationByPostRepository(Social_Media.Data.ContextData Data, ILogger<Repository<InteractionNotificationByPost>> Logger) : base(Data, Logger)
        {
            this.InteractionNotificationByPost = Data.Set<InteractionNotificationByPost>();
        }
    }
}
