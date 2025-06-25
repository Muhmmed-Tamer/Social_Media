using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByCommentRepository : Repository<InteractionNotificationByComment>, IInteractionNotificationByCommentRepository
    {
        private readonly DbSet<InteractionWithComment> InteractionWithComments;
        public InteractionNotificationByCommentRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.InteractionWithComments = Data.Set<InteractionWithComment>();
        }
    }
}
