using Serilog;
using Social_Media.Data.Models.Friends;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Repository;
using Social_Media.Services.AbstractsServices.FriendsServices;

namespace Social_Media.Services.ImplementationServices.FriendServices
{
    public class FriendServices : Services<Friend>, IFriendServices
    {
        private readonly IFriendRepository FriendRepository;
        private readonly ILogger Logger;
        public FriendServices(ILogger Logger, IRepository<Friend> Repository, IFriendRepository FriendRepository) : base(Logger, Repository)
        {
            this.FriendRepository = FriendRepository;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<Friend> Friends)
        {
            try
            {
                await FriendRepository.BulkInsertAsync(Friends);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<string>> GetFriendsIdOFUserByUserIdAsync(string UserId)
        {
            try
            {
                return await FriendRepository.GetFriendsIdOFUserByUserIdAsync(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in GetFriendsIdOFUserByUserIdAsync");
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
