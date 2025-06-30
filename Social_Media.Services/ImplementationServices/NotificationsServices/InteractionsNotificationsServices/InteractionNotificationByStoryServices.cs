using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByStoryServices : Services<InteractionNotificationByStory>, IInteractionNotificationByStoryServices
    {
        public InteractionNotificationByStoryServices(ILogger Logger, IRepository<InteractionNotificationByStory> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
