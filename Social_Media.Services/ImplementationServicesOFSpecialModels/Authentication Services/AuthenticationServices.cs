using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Social_Media.Data.Helpers.DataFromappSettings.JWT;
using Social_Media.Data.Helpers.Models;
using Social_Media.Data.Identity;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.Authentication_Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        #region Properties
        private readonly UserManager<ApplicationUser> ManagerUser;
        private readonly IConfiguration Configuration;
        private readonly JWTSetting JWTSetting;
        private readonly ILogger Logger;
        #endregion
        #region Constructors
        public AuthenticationServices(ILogger Logger, UserManager<ApplicationUser> ManagerUser, IConfiguration Configuration, IOptions<JWTSetting> JWTSetting)
        {
            this.Configuration = Configuration;
            this.JWTSetting = JWTSetting.Value;
            this.ManagerUser = ManagerUser;
            this.Logger = Logger;
        }

        public Task<string> GenerateResetPasswordCode()
        {
            try
            {
                Random Random = new Random();
                return Task.FromResult(Random.Next(0, 1000000).ToString("D6"));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        #endregion

        #region JWTToken
        private JwtSecurityToken ReadJWTThatUserGiveMe(string AccessTokenFromUser)
        {
            try
            {
                JwtSecurityTokenHandler JWTHandler = new JwtSecurityTokenHandler();
                return JWTHandler.ReadJwtToken(AccessTokenFromUser);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<string> GenerateAccessTokenAsync(ApplicationUser User)
        {
            try
            {
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: JWTSetting.Issuer, audience: JWTSetting.Audience,
                    claims: await GetClaims(User), signingCredentials: GetSigningCredentials(),
                    expires: DateTime.Now.AddMinutes(JWTSetting.ExpiresOn));

                JwtSecurityTokenHandler MyToken = new JwtSecurityTokenHandler();
                return MyToken.WriteToken(jwtSecurityToken);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        private async Task<List<Claim>> GetClaims(ApplicationUser User)
        {
            try
            {
                List<Claim> Claims = new();
                Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
                Claims.Add(new Claim(ClaimTypes.Name, User.Name.FirstNameInEnglish + " " + User.Name.LastNameInEnglish));
                Claims.Add(new Claim(ClaimTypes.Email, User.Email!));
                Claims.Add(new Claim(ClaimTypes.MobilePhone, User.PhoneNumber!));
                Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                foreach (var Role in await ManagerUser.GetRolesAsync(User))
                {
                    Claims.Add(new Claim(ClaimTypes.Role, Role));
                }
                return Claims;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        private SigningCredentials GetSigningCredentials()
        {
            try
            {
                SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSetting.SecurityKey));
                return new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}", ex);
                throw;
            }
        }
        public string GenerateRefreshToken()
        {
            try
            {
                byte[] Bytes = new byte[32];
                RandomNumberGenerator RandomNumberGeneratorThatCreated = RandomNumberGenerator.Create();
                RandomNumberGeneratorThatCreated.GetBytes(Bytes);
                return Convert.ToBase64String(Bytes);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<JWTTokenResult> GenerateToken(ApplicationUser User)
        {
            try
            {
                return new JWTTokenResult()
                {
                    AccessToken = await GenerateAccessTokenAsync(User),
                    RefreshToken = GenerateRefreshToken(),
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public string GetIdOFJWT(string AccessTokenFromUser)
        {
            try
            {
                JwtSecurityToken JWTSecurityTokenOFUser = ReadJWTThatUserGiveMe(AccessTokenFromUser);
                return JWTSecurityTokenOFUser.Id;
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
