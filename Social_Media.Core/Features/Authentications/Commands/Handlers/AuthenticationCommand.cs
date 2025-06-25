using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Authentications.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Features.Authentications.Commands.Handlers
{
    public class AuthenticationCommand : ResponseHandler, IRequestHandler<SignInCommand, Response<string>>,
        IRequestHandler<ResendConfirmationEmailCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public AuthenticationCommand(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToLogin = await UnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                bool AccountIsConfirmed = await UnitOFWork.UserServices.ManagerUser.IsEmailConfirmedAsync(UserThatWantToLogin);
                if (!AccountIsConfirmed)
                {
                    return BadRequest<string>("Your Account Is Not Confirmed, Please Check Your Email For Confirmation Link");
                }
                if (UserThatWantToLogin is not null)
                {
                    bool PasswordIsTrue = await UnitOFWork.UserServices.ManagerUser.CheckPasswordAsync(UserThatWantToLogin, request.User_Password);
                    if (PasswordIsTrue)
                    {
                        string TokenOFUserThatWantToLogin = await UnitOFWork.AuthenticationServices.GenerateTokenAsync(UserThatWantToLogin);
                        return OK<string>(TokenOFUserThatWantToLogin);
                    }
                }
                return BadRequest<string>("Email Or Password Is Wrong");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResendConfirmationEmail = await UnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                if (UserThatWantToResendConfirmationEmail is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                bool AccountIsConfirmed = await UnitOFWork.UserServices.ManagerUser.IsEmailConfirmedAsync(UserThatWantToResendConfirmationEmail);
                if (AccountIsConfirmed)
                {
                    return BadRequest<string>("Your Account Is Already Confirmed");
                }
                string ConfirmationToken = await UnitOFWork.UserServices.ManagerUser.GenerateEmailConfirmationTokenAsync(UserThatWantToResendConfirmationEmail);
                bool EmailIsSentSuccessfully = await UnitOFWork.EmailServices.
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
    }
}
