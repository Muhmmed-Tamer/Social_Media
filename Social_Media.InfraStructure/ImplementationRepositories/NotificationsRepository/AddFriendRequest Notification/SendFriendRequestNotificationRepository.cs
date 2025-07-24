using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.AddFriendRequest_Notification
{
    public class SendFriendRequestNotificationRepository : Repository<SendFriendRequestNotification>, ISendFriendRequestNotificationRepository
    {
        private readonly DbSet<SendFriendRequestNotification> SendFriendRequestNotification;
        private readonly ILogger Logger;
        public SendFriendRequestNotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.SendFriendRequestNotification = Data.Set<SendFriendRequestNotification>();
            this.Logger = Logger;
        }

        public async Task<SendFriendRequestNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await SendFriendRequestNotification.Where(SN => SN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
