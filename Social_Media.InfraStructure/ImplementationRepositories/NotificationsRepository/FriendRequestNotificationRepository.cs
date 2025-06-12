using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class FriendRequestNotificationRepository : Repository<FriendRequestNotification>, IFriendRequestNotificationRepository
    {
        private readonly DbSet<FriendRequestNotification> FriendRequestNotification;
        public FriendRequestNotificationRepository(Social_Media.Data.ContextData Data, ILogger<Repository<FriendRequestNotification>> Logger) : base(Data, Logger)
        {
            this.FriendRequestNotification = Data.Set<FriendRequestNotification>();
        }
    }
}
