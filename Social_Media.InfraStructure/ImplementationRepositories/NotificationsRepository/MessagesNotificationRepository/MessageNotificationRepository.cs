using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.MessagesNotificationRepository
{
    public class MessageNotificationRepository : Repository<MessageNotification>, IMessageNotificationRepository
    {
        private readonly DbSet<MessageNotification> MessageNotification;
        private readonly ILogger Logger;
        public MessageNotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            MessageNotification = Data.Set<MessageNotification>();
        }

        public async Task<MessageNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await MessageNotification.Where(MN => MN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
