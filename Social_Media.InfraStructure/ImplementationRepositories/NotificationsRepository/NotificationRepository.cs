using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly DbSet<Notification> Notification;
        private readonly ILogger Logger;
        public NotificationRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Notification = Data.Set<Notification>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(IEnumerable<Notification> Notifications)
        {
            try
            {
                await Notification.BulkInsertAsync(Notifications);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<Notification>> GetAllNotificationOFUser(string UserId)
        {
            try
            {
                return await Notification.Where(N => N.UserIdWhoReceivedTheNotification == UserId)
                    .Include(N => N.PostNotifications)
                    .Include(N => N.InteractionNotificationByComments)
                    .Include(N => N.ConfirmFriendRequestNotifications)
                    .Include(N => N.InteractionNotificationByPosts)
                    .Include(N => N.InteractionNotificationByStories)
                    .Include(N => N.SendFriendRequestNotifications)
                    .Include(N => N.UserThatCausedNotification)
                    .Include(N => N.MessageNotifications)
                    .Include(N => N.CommentNotifications)
                    .Include(N => N.MessageNotifications)
                    .OrderByDescending(N => N.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<IQueryable<Notification>> GetAllNotificationOFUserAsQueryable(string UserId)
        {
            try
            {
                return Notification.Where(N => N.UserIdWhoReceivedTheNotification == UserId)
                    .Include(N => N.PostNotifications)
                    .Include(N => N.InteractionNotificationByComments)
                    .Include(N => N.ConfirmFriendRequestNotifications)
                    .Include(N => N.InteractionNotificationByPosts)
                    .Include(N => N.InteractionNotificationByStories)
                    .Include(N => N.SendFriendRequestNotifications)
                    .Include(N => N.UserThatCausedNotification)
                    .Include(N => N.MessageNotifications)
                    .Include(N => N.CommentNotifications)
                    .Include(N => N.MessageNotifications)
                    .OrderByDescending(N => N.CreatedAt)
                    .AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
