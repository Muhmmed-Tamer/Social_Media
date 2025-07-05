using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByCommentServices : Services<InteractionNotificationByComment>, IInteractionNotificationByCommentServices
    {
        private readonly IInteractionNotificationByCommentRepository InteractionNotificationByComment;
        private readonly ILogger Logger;
        public InteractionNotificationByCommentServices(ILogger Logger, IRepository<InteractionNotificationByComment> Repository, IInteractionNotificationByCommentRepository InteractionNotificationByComment) : base(Logger, Repository)
        {
            this.InteractionNotificationByComment = InteractionNotificationByComment;
            this.Logger = Logger;
        }

        public async Task<InteractionNotificationByComment> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionNotificationByComment.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override Method Here
    }
}
