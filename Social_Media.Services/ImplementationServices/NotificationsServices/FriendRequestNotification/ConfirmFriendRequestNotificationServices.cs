using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification
{
    public class ConfirmFriendRequestNotificationServices : Services<ConfirmFriendRequestNotification>, IConfirmFriendRequestNotificationServices
    {
        private readonly IConfirmFriendRequestNotificationRepository ConfirmFriendRequestNotification;
        private readonly ILogger Logger;
        public ConfirmFriendRequestNotificationServices(ILogger Logger, IRepository<ConfirmFriendRequestNotification> Repository, IConfirmFriendRequestNotificationRepository ConfirmFriendRequestNotification) : base(Logger, Repository)
        {
            this.ConfirmFriendRequestNotification = ConfirmFriendRequestNotification;
            this.Logger = Logger;
        }

        public async Task<ConfirmFriendRequestNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await ConfirmFriendRequestNotification.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
