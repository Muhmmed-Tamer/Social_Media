using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Friends;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class FriendRequestRepository : Repository<FriendRequest>, IFriendRequestRepository
    {
        private readonly DbSet<FriendRequest> FriendRequest;
        private readonly ILogger Logger;
        public FriendRequestRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.FriendRequest = Data.Set<FriendRequest>();
            this.Logger = Logger;
        }

        public async Task<FriendRequest> GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(string UserId, string FriendUserId)
        {
            try
            {
                return await FriendRequest.Where(FRN => (FRN.FriendUserId == FriendUserId && FRN.UserId == UserId) || (FRN.FriendUserId == UserId && FRN.UserId == FriendUserId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetFriendThatWantToChangeFriendRequestStatusAsync method of FriendRequestNotificationRepository");
                throw;
            }
        }
    }
}
