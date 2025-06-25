using Social_Media.Data.Identity;

namespace Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services
{
    public interface IAuthenticationServices
    {
        Task<string> GenerateTokenAsync(ApplicationUser User);
        Task<string> GenerateResetPasswordCode();
    }
}
