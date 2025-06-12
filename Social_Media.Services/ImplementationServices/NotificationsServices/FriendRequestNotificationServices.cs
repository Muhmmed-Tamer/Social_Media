using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class FriendRequestNotificationServices : Services<FriendRequestNotification>, IFriendRequestNotificationServices
    {
        public FriendRequestNotificationServices(ILogger<Services<FriendRequestNotification>> Logger, IRepository<FriendRequestNotification> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
