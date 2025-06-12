using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class PostNotificationServices : Services<PostNotification>, IPostNotificationServices
    {
        public PostNotificationServices(ILogger<Services<PostNotification>> Logger, IRepository<PostNotification> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override ethods Here
    }
}
