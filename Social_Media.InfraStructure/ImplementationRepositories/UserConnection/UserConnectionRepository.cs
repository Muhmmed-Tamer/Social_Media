using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.UserConnection
{
    public class UserConnectionRepository : Repository<Data.Models.UserConnection>, IUserConnectionRepository
    {
        private readonly ILogger Logger;
        private readonly Data.ContextData Data;
        public UserConnectionRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Logger = Logger;
            this.Data = Data;
        }

        public async Task DeleteByConnectionId(string ConnectionId)
        {
            try
            {
                if (await GetByConnectionId(ConnectionId) is not null)
                {
                    Data.UserConnections.Remove(await GetByConnectionId(ConnectionId));
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
                if (await GetByUserId(UserId) is not null)
                {
                    Data.UserConnections.Remove(await GetByUserId(UserId));
                }
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
                return await Data.UserConnections.FirstOrDefaultAsync(UC => UC.ConnectionId == ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }


        public async Task<Data.Models.UserConnection?> GetByUserId(string UserId)
        {
            try
            {
                return await Data.UserConnections.FirstOrDefaultAsync(UC => UC.UserId == UserId);
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
    }
}
