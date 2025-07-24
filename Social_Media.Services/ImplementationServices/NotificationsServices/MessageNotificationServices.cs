using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class MessageNotificationServices : Services<MessageNotification>, IMessageNotificationServices
    {
        private readonly IMessageNotificationRepository MessageNotification;
        private readonly ILogger Logger;
        public MessageNotificationServices(ILogger Logger, IRepository<MessageNotification> Repository, IMessageNotificationRepository MessageNotification) : base(Logger, Repository)
        {
            this.MessageNotification = MessageNotification;
            this.Logger = Logger;
        }

        public async Task<MessageNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await MessageNotification.GetByNotificationId(NotificationId);
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
