using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class MessageNotificationRepository : Repository<MessageNotification>, IMessageNotificationRepository
    {
        private readonly DbSet<MessageNotification> MessageNotification;
        public MessageNotificationRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.MessageNotification = Data.Set<MessageNotification>();
        }
    }
}
