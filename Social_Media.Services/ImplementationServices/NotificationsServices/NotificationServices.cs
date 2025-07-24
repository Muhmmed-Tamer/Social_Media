using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.Services.AbstractsServices.INotificationsServices;

namespace Social_Media.Services.ImplementationServices.NotificationsServices
{
    public class NotificationServices : Services<Notification>, INotificationServices
    {
        private readonly INotificationRepository NotificationRepository;
        private readonly ILogger Logger;
        public NotificationServices(ILogger Logger, IRepository<Notification> Repository, INotificationRepository NotificationRepository) : base(Logger, Repository)
        {
            this.NotificationRepository = NotificationRepository;
            this.Logger = Logger;
        }

        public Task BulkInsertAsync(IEnumerable<Notification> Notifications)
        {
            try
            {
                return NotificationRepository.BulkInsertAsync(Notifications);
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
                return await NotificationRepository.GetAllNotificationOFUser(UserId);
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
                return await NotificationRepository.GetAllNotificationOFUserAsQueryable(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }

        }
    }
}
