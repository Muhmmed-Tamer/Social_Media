using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByCommentServices : Services<InteractionNotificationByComment>, IInteractionNotificationByCommentServices
    {
        public InteractionNotificationByCommentServices(ILogger Logger, IRepository<InteractionNotificationByComment> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Method Here
    }
}
