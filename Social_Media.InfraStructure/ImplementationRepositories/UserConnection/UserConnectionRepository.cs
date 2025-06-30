using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.UserConnection
{
    public class UserConnectionRepository : Repository<Data.Models.UserConnection>, IUserConnectionRepository
    {
        #region Property & Attributes
        private readonly ILogger Logger;
        private readonly Data.ContextData Data;
        #endregion
        #region Constructors
        public UserConnectionRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            this.Data = Data;
        }
        #endregion
        #region Methods
        public async Task DeleteByConnectionId(string ConnectionId)
        {
            try
            {
                Data.Models.UserConnection? UserConnectionThatWantToDelete = await GetByConnectionId(ConnectionId);
                if (UserConnectionThatWantToDelete is not null)
                {
                    Data.UserConnections.Remove(UserConnectionThatWantToDelete);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task DeleteByUserId(string UserId)
        {
            try
            {
                List<Data.Models.UserConnection> AllUserConnectionOFUser = await GetByUserId(UserId);
                if (AllUserConnectionOFUser is not null && AllUserConnectionOFUser.Count > 0)
                {
                    Data.UserConnections.RemoveRange(AllUserConnectionOFUser);
                }
                return;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<Data.Models.UserConnection?> GetByConnectionId(string ConnectionId)
        {
            try
            {
                return await Data.UserConnections.Where(UC => UC.ConnectionId == ConnectionId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }


        public async Task<List<Data.Models.UserConnection>?> GetByUserId(string UserId)
        {
            try
            {
                return await Data.UserConnections.Where(UC => UC.UserId == UserId).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }


        public async Task<List<string>> GetConnectionIdOFFriends(string UserId)
        {
            try
            {
                List<string> ConnectionIdOFFriends = await Data.UserConnections.Include(UC => UC.User)
                   .Include(U => U.User.FriendshipsInitiated.Where(F => F.UserId == UserId))
                   .Select(UC => UC.ConnectionId).ToListAsync();
                return ConnectionIdOFFriends;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<bool> UserIsOnlineOrNot(string UserId)
        {
            try
            {
                List<Data.Models.UserConnection> UserConnectionOFUser = await GetByUserId(UserId);
                return UserConnectionOFUser is not null && UserConnectionOFUser.Count > 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
        #endregion
    }
}
