using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class PostNotificationRepository : Repository<PostNotification>, IPostNotificationRepository
    {
        private readonly DbSet<PostNotification> PostNotification;
        public PostNotificationRepository(Social_Media.Data.ContextData Data, ILogger<Repository<PostNotification>> Logger) : base(Data, Logger)
        {
            this.PostNotification = Data.Set<PostNotification>();
        }
    }
}
