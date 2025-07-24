using MediatR;
using Microsoft.Extensions.Options;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Authentications.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Helpers.DataFromappSettings.JWT;
using Social_Media.Data.Helpers.Models;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Identity;

namespace Social_Media.Core.Features.Authentications.Commands.Handlers
{
    public class AuthenticationCommand : ResponseHandler, IRequestHandler<SignInCommand, Response<JWTTokenResult>>,
        IRequestHandler<ResendConfirmationEmailCommand, Response<string>>,
        IRequestHandler<RefreshTokenCommand, Response<JWTTokenResult>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly JWTSetting JWTSetting;
        private readonly ILogger Logger;
        public AuthenticationCommand(ILogger Logger, IUnitOFWork UnitOFWork, IOptions<JWTSetting> JWTSetting)
        {
            this.UnitOFWork = UnitOFWork;
            this.JWTSetting = JWTSetting.Value;
            this.Logger = Logger;
        }
        public async Task<Response<JWTTokenResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JWTTokenResult ResultToken = new JWTTokenResult();
                ApplicationUser UserThatWantToLogin = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                bool AccountIsConfirmed = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.IsEmailConfirmedAsync(UserThatWantToLogin);
                if (!AccountIsConfirmed)
                {
                    return BadRequest<JWTTokenResult>("Your Account Is Not Confirmed, Please Check Your Email For Confirmation Link");
                }
                if (UserThatWantToLogin is not null)
                {
                    if (UserThatWantToLogin.IsDeleted)
                    {
                        return BadRequest<JWTTokenResult>("User Is Not Found");
                    }
                    bool PasswordIsTrue = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.CheckPasswordAsync(UserThatWantToLogin, request.User_Password);
                    if (PasswordIsTrue)
                    {
                        //Check If User Have A Valid RefreshToken 
                        UserRefreshToken UserRefreshToken = await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.GetAllUserRefreshTokenByUserIdAsync(UserThatWantToLogin.Id);
                        if (UserRefreshToken is not null)
                        {
                            if (await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.RefreshTokenIsActiveOrNot(UserRefreshToken))
                            {
                                UserRefreshToken.AccessToken = await UnitOFWork.AuthenticationServices.GenerateAccessTokenAsync(UserRefreshToken.User);
                                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.UpdateAsync(UserRefreshToken);
                                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.SaveChangesAsync();
                                ResultToken = UnitOFWork.Mapper.Map<JWTTokenResult>(UserRefreshToken);
                            }
                            else
                            {
                                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.DeleteAsync(UserRefreshToken.Id);
                                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.SaveChangesAsync();
                                ResultToken = await GetSignInCommandResult(UserRefreshToken.User);
                            }
                        }
                        else
                        {
                            ResultToken = await GetSignInCommandResult(UserThatWantToLogin);
                        }
                        return OK(ResultToken);
                    }
                }
                return BadRequest<JWTTokenResult>("Email Or Password Is Wrong");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<JWTTokenResult>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResendConfirmationEmail = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                if (UserThatWantToResendConfirmationEmail is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                bool AccountIsConfirmed = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.IsEmailConfirmedAsync(UserThatWantToResendConfirmationEmail);
                if (AccountIsConfirmed)
                {
                    return BadRequest<string>("Your Account Is Already Confirmed");
                }
                string ConfirmationToken = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.GenerateEmailConfirmationTokenAsync(UserThatWantToResendConfirmationEmail);
                bool EmailIsSentSuccessfully = await UnitOFWork.ExternalNotificationUnitOFWork.EmailServices.
                       SendEmailAsync("Confirm Your Email", $"Please confirm your email by clicking <a href='{UnitOFWork.ProtocolAndHostServices.GoToEndPointInMyApplication("Authentication", "ConfirmEmail", new { EmailToken = ConfirmationToken, UserId = UserThatWantToResendConfirmationEmail.Id })}'>here</a>.", UserThatWantToResendConfirmationEmail.Email);
                if (EmailIsSentSuccessfully)
                {
                    return Created<string>("Confirm Email Is Send Successfully");
                }
                return BadRequest<string>("Some Thing Is Error when I Sent Confirm Email");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<JWTTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JWTTokenResult ResultToken = new JWTTokenResult();
                UserRefreshToken UserRefreshToken = await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.GetAllUserRefreshTokensByAccessAndRefreshTokenAsync(request.UserAccessToken, request.UserRefreshToken);

                if (UserRefreshToken is null)
                {
                    return BadRequest<JWTTokenResult>("Invalid Token");
                }
                if (await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.RefreshTokenIsActiveOrNot(UserRefreshToken))
                {
                    //RefreshTokenIsActive (I Will Update AccessToken and Return Access & Refresh Token)
                    UserRefreshToken.AccessToken = await UnitOFWork.AuthenticationServices.GenerateAccessTokenAsync(UserRefreshToken.User);
                    await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.UpdateAsync(UserRefreshToken);
                    await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.SaveChangesAsync();
                    ResultToken = UnitOFWork.Mapper.Map<JWTTokenResult>(UserRefreshToken);
                }
                else
                {
                    await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.DeleteAsync(UserRefreshToken.Id);
                    await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.SaveChangesAsync();
                    ResultToken = await GetSignInCommandResult(UserRefreshToken.User);
                }
                return OK(ResultToken);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        private async Task<JWTTokenResult> GetSignInCommandResult(ApplicationUser User)
        {
            try
            {
                JWTTokenResult JWTTokenOFUser = await UnitOFWork.AuthenticationServices.GenerateToken(User);
                UserRefreshToken Mapped_UserRefreshToken = UnitOFWork.Mapper.Map<UserRefreshToken>(JWTTokenOFUser);
                Mapped_UserRefreshToken.UserId = User.Id;
                Mapped_UserRefreshToken.JWTId = UnitOFWork.AuthenticationServices.GetIdOFJWT(JWTTokenOFUser.AccessToken);
                Mapped_UserRefreshToken.ExpirationDate = DateTime.Now.AddMinutes(JWTSetting.RefreshTokenExpiredOn);

                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.AddAsync(Mapped_UserRefreshToken);
                await UnitOFWork.IdentityUnitOFWork.UserRefreshTokenService.SaveChangesAsync();

                return JWTTokenOFUser;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
