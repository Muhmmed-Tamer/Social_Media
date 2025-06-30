using Social_Media.Data.Helpers.Models;
using Social_Media.Data.Identity;

namespace Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services
{
    public interface IAuthenticationServices
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser User);
        Task<JWTTokenResult> GenerateToken(ApplicationUser User);
        string GetIdOFJWT(string AccessTokenFromUser);
        Task<string> GenerateResetPasswordCode();
        string GenerateRefreshToken();
    }
}
