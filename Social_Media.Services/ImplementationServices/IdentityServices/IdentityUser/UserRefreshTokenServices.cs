using Serilog;
using Social_Media.Data.Models.Identity;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.IdentityUser_Repository;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Services.ImplementationServices.IdentityServices.IdentityUser
{
    public class UserRefreshTokenServices : Services<UserRefreshToken>, IUserRefreshTokenServices
    {
        private readonly IUserRefreshTokenRepository UserRefreshTokenRepository;
        private readonly ILogger Logger;

        public UserRefreshTokenServices(ILogger Logger, IRepository<UserRefreshToken> Repository, IUserRefreshTokenRepository UserRefreshTokenRepository) : base(Logger, Repository)
        {
            this.UserRefreshTokenRepository = UserRefreshTokenRepository;
            this.Logger = Logger;
        }

        public async Task<UserRefreshToken> GetAllUserRefreshTokensByAccessAndRefreshTokenAsync(string AccessToken, string RefreshToken)
        {
            try
            {
                return await UserRefreshTokenRepository.GetAllUserRefreshTokensByAccessAndRefreshTokenAsync(AccessToken, RefreshToken);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<UserRefreshToken> GetAllUserRefreshTokenByUserIdAsync(string UserId)
        {
            try
            {
                return await UserRefreshTokenRepository.GetAllUserRefreshTokenByUserIdAsync(UserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<bool> RefreshTokenIsActiveOrNot(UserRefreshToken UserRefreshToken)
        {
            try
            {
                return await UserRefreshTokenRepository.RefreshTokenIsActiveOrNot(UserRefreshToken);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override  Of Any Method
    }
}
