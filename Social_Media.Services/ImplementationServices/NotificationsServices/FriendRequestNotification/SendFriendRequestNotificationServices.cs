using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification
{
    public class SendFriendRequestNotificationServices : Services<SendFriendRequestNotification>, ISendFriendRequestNotificationServices
    {
        private readonly ISendFriendRequestNotificationRepository SendFriendRequestNotification;
        private readonly ILogger Logger;
        public SendFriendRequestNotificationServices(ILogger Logger, IRepository<SendFriendRequestNotification> Repository, ISendFriendRequestNotificationRepository SendFriendRequestNotification) : base(Logger, Repository)
        {
            this.SendFriendRequestNotification = SendFriendRequestNotification;
            this.Logger = Logger;
        }

        public async Task<SendFriendRequestNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await SendFriendRequestNotification.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
