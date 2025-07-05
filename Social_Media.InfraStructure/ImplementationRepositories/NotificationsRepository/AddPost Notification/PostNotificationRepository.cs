using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class PostNotificationRepository : Repository<PostNotification>, IPostNotificationRepository
    {
        private readonly DbSet<PostNotification> PostNotification;
        private readonly ILogger Logger;
        public PostNotificationRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.PostNotification = Data.Set<PostNotification>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(IEnumerable<PostNotification> PostNotifications)
        {
            try
            {
                await PostNotification.BulkInsertAsync(PostNotifications);
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
                return await PostNotification.Where(PN => PN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
