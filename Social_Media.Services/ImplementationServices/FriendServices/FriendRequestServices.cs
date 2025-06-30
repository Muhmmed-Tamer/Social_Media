using Serilog;
using Social_Media.Data.Models.Friends;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices.FriendsServices;

namespace Social_Media.Services.ImplementationServices.FriendServices
{
    public class FriendRequestServices : Services<FriendRequest>, IFriendRequestServices
    {
        private readonly IFriendRequestRepository FriendRequestRepository;
        private readonly ILogger Logger;
        public FriendRequestServices(ILogger Logger, IRepository<FriendRequest> Repository, IFriendRequestRepository FriendRequestRepository) : base(Logger, Repository)
        {
            this.FriendRequestRepository = FriendRequestRepository;
            this.Logger = Logger;
        }
        //You Can Override Methods Here
        public override async Task AddAsync(FriendRequest entity)
        {
            try
            {
                // Ensure that the UserId and FriendUserId are not the same before adding the notification
                if (entity.UserId != entity.FriendUserId)
                {
                    await base.AddAsync(entity);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error adding FriendRequestNotification");
                throw;
            }
        }

        public async Task<FriendRequest> GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(string UserId, string FriendUserId)
        {
            try
            {
                return await FriendRequestRepository.GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(FriendUserId, UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetFriendThatWantToChangeFriendRequestStatusAsync method of FriendRequestNotificationServices");
                throw;
            }
        }
    }
}
