using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByStoryServices : Services<InteractionNotificationByStory>, IInteractionNotificationByStoryServices
    {
        private readonly IInteractionNotificationByStoryRepository InteractionNotificationByStory;
        private readonly ILogger Logger;
        public InteractionNotificationByStoryServices(ILogger Logger, IRepository<InteractionNotificationByStory> Repository, IInteractionNotificationByStoryRepository interactionNotificationByStory, ILogger logger) : base(Logger, Repository)
        {
            this.InteractionNotificationByStory = interactionNotificationByStory;
            this.Logger = logger;
        }

        public async Task<InteractionNotificationByStory> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionNotificationByStory.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
