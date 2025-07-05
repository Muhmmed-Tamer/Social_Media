using Serilog;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class PostNotificationServices : Services<PostNotification>, IPostNotificationServices
    {
        private readonly IPostNotificationRepository PostNotificationRepository;
        private readonly ILogger Logger;
        public PostNotificationServices(ILogger Logger, IRepository<PostNotification> Repository, IPostNotificationRepository PostNotificationRepository) : base(Logger, Repository)
        {
            this.PostNotificationRepository = PostNotificationRepository;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(IEnumerable<PostNotification> PostNotifications)
        {
            try
            {
                await PostNotificationRepository.BulkInsertAsync(PostNotifications);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<PostNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await PostNotificationRepository.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override ethods Here
    }
}
