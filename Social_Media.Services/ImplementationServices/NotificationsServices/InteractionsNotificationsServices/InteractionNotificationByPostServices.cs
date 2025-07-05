using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByPostServices : Services<InteractionNotificationByPost>, IInteractionNotificationByPostServices
    {
        private readonly IInteractionNotificationByPostRepository InteractionNotificationByPost;
        private readonly ILogger Logger;
        public InteractionNotificationByPostServices(ILogger Logger, IRepository<InteractionNotificationByPost> Repository, IInteractionNotificationByPostRepository interactionNotificationByPost, ILogger logger) : base(Logger, Repository)
        {
            this.InteractionNotificationByPost = interactionNotificationByPost;
            this.Logger = logger;
        }

        public async Task<InteractionNotificationByPost> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionNotificationByPost.GetByNotificationId(NotificationId);
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
