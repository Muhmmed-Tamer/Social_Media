using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class CommentNotificationRepository : Repository<CommentNotification>, ICommentNotificationRepository
    {
        private readonly DbSet<CommentNotification> CommentNotification;
        private readonly ILogger Logger;
        public CommentNotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.CommentNotification = Data.Set<CommentNotification>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<CommentNotification> CommentNotifications)
        {
            try
            {
                await this.CommentNotification.BulkInsertAsync(CommentNotifications);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<CommentNotification> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await CommentNotification.Where(CN => CN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
