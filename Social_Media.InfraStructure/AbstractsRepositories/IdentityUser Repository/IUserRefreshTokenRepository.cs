using Social_Media.Data.Models.Identity;

namespace Social_Media.InfraStructure.AbstractsRepositories.IdentityUser_Repository
{
    public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken>
    {
        Task<UserRefreshToken> GetAllUserRefreshTokenByUserIdAsync(string UserId);
        Task<UserRefreshToken> GetAllUserRefreshTokensByAccessAndRefreshTokenAsync(string AccessToken, string RefreshToken);
        Task<bool> RefreshTokenIsActiveOrNot(UserRefreshToken UserRefreshToken);
    }
}
