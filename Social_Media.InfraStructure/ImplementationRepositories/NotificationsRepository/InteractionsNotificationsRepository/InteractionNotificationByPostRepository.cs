using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByPostRepository : Repository<InteractionNotificationByPost>, IInteractionNotificationByPostRepository
    {
        private readonly DbSet<InteractionNotificationByPost> InteractionNotificationByPost;
        private readonly ILogger Logger;
        public InteractionNotificationByPostRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            this.InteractionNotificationByPost = Data.Set<InteractionNotificationByPost>();
        }

        public async Task<InteractionNotificationByPost> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionNotificationByPost.Where(IN => IN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
