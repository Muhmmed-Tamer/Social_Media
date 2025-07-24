using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByCommentRepository : Repository<InteractionNotificationByComment>, IInteractionNotificationByCommentRepository
    {
        private readonly DbSet<InteractionNotificationByComment> InteractionWithComments;
        private readonly ILogger Logger;
        public InteractionNotificationByCommentRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            this.InteractionWithComments = Data.Set<InteractionNotificationByComment>();
        }

        public async Task<InteractionNotificationByComment> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionWithComments.Where(IN => IN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
