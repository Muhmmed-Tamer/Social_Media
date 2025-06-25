using Serilog;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.AddFriendRequest_Notification
{
    public class SendFriendRequestNotificationRepository : Repository<SendFriendRequestNotification>, ISendFriendRequestNotificationRepository
    {
        public SendFriendRequestNotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
        }
    }
}
