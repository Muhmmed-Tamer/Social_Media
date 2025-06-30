using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification
{
    public class ConfirmFriendRequestNotificationServices : Services<ConfirmFriendRequestNotification>, IConfirmFriendRequestNotificationServices
    {
        public ConfirmFriendRequestNotificationServices(ILogger Logger, IRepository<ConfirmFriendRequestNotification> Repository) : base(Logger, Repository)
        {
        }
    }
}
