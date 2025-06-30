using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Friends;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        private readonly Serilog.ILogger Logger;
        private readonly DbSet<Friend> Friend;
        public FriendRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            this.Friend = Data.Set<Friend>();
        }

        public async Task BulkInsertAsync(List<Friend> Friends)
        {
            try
            {
                await Friend.BulkInsertAsync(Friends);
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
                return await Friend.Where(F => F.UserId == UserId).Select(F => F.FriendUserId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
