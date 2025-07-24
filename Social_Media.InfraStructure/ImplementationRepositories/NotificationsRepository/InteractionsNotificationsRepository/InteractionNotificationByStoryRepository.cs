using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository
{
    public class InteractionNotificationByStoryRepository : Repository<InteractionNotificationByStory>, IInteractionNotificationByStoryRepository
    {
        private readonly DbSet<InteractionNotificationByStory> InteractionNotificationByStory;
        private readonly ILogger Logger;
        public InteractionNotificationByStoryRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.InteractionNotificationByStory = Data.Set<InteractionNotificationByStory>();
        }

        public async Task<InteractionNotificationByStory> GetByNotificationId(int NotificationId)
        {
            try
            {
                return await InteractionNotificationByStory.Where(IN => IN.NotificationId == NotificationId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
