using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Identity;
using Social_Media.InfraStructure.AbstractsRepositories.IdentityUser_Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories.IdentityUser_Repository
{
    public class UserRefreshTokenRepository : Repository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        private readonly DbSet<UserRefreshToken> UserRefreshToken;
        private readonly ILogger Logger;
        public UserRefreshTokenRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.UserRefreshToken = Data.Set<UserRefreshToken>();
            this.Logger = Logger;
        }

        public async Task<UserRefreshToken> GetAllUserRefreshTokensByAccessAndRefreshTokenAsync(string AccessToken, string RefreshToken)
        {
            try
            {
                return await UserRefreshToken.Where(URT => URT.AccessToken == AccessToken && URT.RefreshToken == RefreshToken).Include(U => U.User).FirstOrDefaultAsync();
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
                return await UserRefreshToken.Where(URT => URT.UserId == UserId).FirstOrDefaultAsync();
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
                return UserRefreshToken.ExpirationDate >= DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
