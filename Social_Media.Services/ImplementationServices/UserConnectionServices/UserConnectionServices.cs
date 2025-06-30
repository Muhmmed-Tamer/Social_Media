using Serilog;
using Social_Media.Data.Models;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.Services.AbstractsServices.UserConnectionServices;

namespace Social_Media.Services.ImplementationServices.UserConnectionServices
{
    public class UserConnectionServices : Services<UserConnection>, IUserConnectionServices
    {
        #region Property & Attributes
        private readonly ILogger Logger;
        private readonly IUserConnectionRepository UserConnectionRepository;
        #endregion
        #region Constructors
        public UserConnectionServices(ILogger Logger, IRepository<UserConnection> Repository, IUserConnectionRepository UserConnectionRepository) : base(Logger, Repository)
        {
            this.Logger = Logger;
            this.UserConnectionRepository = UserConnectionRepository;
        }
        #endregion
        #region Methods
        public async Task DeleteByConnectionId(string ConnectionId)
        {
            try
            {
                await UserConnectionRepository.DeleteByConnectionId(ConnectionId);
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
                await UserConnectionRepository.DeleteByUserId(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<UserConnection?> GetByConnectionId(string ConnectionId)
        {
            try
            {
                return await UserConnectionRepository.GetByConnectionId(ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<UserConnection>?> GetByUserId(string UserId)
        {
            try
            {
                return await UserConnectionRepository.GetByUserId(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }


        public Task<List<string>> GetConnectionIdOFFriends(string UserId)
        {
            try
            {
                return UserConnectionRepository.GetConnectionIdOFFriends(UserId);
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
                return await UserConnectionRepository.UserIsOnlineOrNot(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion
    }
}
