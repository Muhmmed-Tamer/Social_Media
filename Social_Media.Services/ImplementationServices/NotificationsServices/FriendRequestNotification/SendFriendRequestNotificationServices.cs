using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification
{
    public class SendFriendRequestNotificationServices : Services<SendFriendRequestNotification>, ISendFriendRequestNotificationServices
    {
        public SendFriendRequestNotificationServices(ILogger Logger, IRepository<SendFriendRequestNotification> Repository) : base(Logger, Repository)
        {
        }
    }
}
