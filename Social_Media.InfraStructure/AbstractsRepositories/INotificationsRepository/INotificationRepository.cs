using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task BulkInsertAsync(IEnumerable<Notification> Notifications);
        Task<List<Notification>> GetAllNotificationOFUser(string UserId);
        Task<IQueryable<Notification>> GetAllNotificationOFUserAsQueryable(string UserId);
    }
}
