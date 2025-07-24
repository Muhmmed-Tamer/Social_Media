using Serilog;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;

namespace Social_Media.Services.ImplementationServices.NotificationsServices
{
    public class CommentNotificationServices : Services<CommentNotification>, ICommentNotificationServices
    {
        private readonly ICommentNotificationRepository CommentNotificationRepository;
        private readonly ILogger Logger;
        public CommentNotificationServices(ILogger Logger, IRepository<CommentNotification> Repository, ICommentNotificationRepository CommentNotificationRepository) : base(Logger, Repository)
        {
            this.CommentNotificationRepository = CommentNotificationRepository;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<CommentNotification> CommentNotifications)
        {
            try
            {
                await CommentNotificationRepository.BulkInsertAsync(CommentNotifications);
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
                return await CommentNotificationRepository.GetByNotificationId(NotificationId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
