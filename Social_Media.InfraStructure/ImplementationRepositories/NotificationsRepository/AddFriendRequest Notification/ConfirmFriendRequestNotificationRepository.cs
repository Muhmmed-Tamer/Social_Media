using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.AddFriendRequest_Notification
{
    public class ConfirmFriendRequestNotificationRepository : Repository<ConfirmFriendRequestNotification>, IConfirmFriendRequestNotificationRepository
    {
        public ConfirmFriendRequestNotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
        }
    }
}
