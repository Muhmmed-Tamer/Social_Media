using MediatR;
using Microsoft.AspNetCore.Identity;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Authentications.Queries.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Features.Authentications.Queries.Handlers
{
    public class AuthenticationQuery : ResponseHandler, IRequestHandler<ConfirmEmailQuery, Response<string>>,
        IRequestHandler<SendResetPasswordByEmailQuery, Response<string>>, IRequestHandler<SendResetPasswordByPhoneNumberQuery, Response<string>>
    {
        private readonly Serilog.ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public AuthenticationQuery(IUnitOFWork UnitOFWork, Serilog.ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IdentityResult EmailIsConfirmedSuccessfully = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.ConfirmEmailAsync(await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId), request.Token);
                if (EmailIsConfirmedSuccessfully.Succeeded)
                {
                    return OK("Email Confirmed Successfully");
                }
                return BadRequest<string>("Invalid Or Expired Token");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(SendResetPasswordByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                //Generate Token For Reset Password
                string CodeOfResetPassword = await UnitOFWork.AuthenticationServices.GenerateResetPasswordCode();
                UserThatWantToResetPassword.ResetPasswordCode = CodeOfResetPassword;
                bool CodeToResetPasswordIsSendSuccessfullyOrNot = await UnitOFWork.ExternalNotificationUnitOFWork.EmailServices.SendEmailAsync("Reset Password", $"Your reset password code is: {CodeOfResetPassword}", UserThatWantToResetPassword.Email);
                if (CodeToResetPasswordIsSendSuccessfullyOrNot)
                {
                    await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToResetPassword);
                    return OK("Reset Password Code Sent Successfully");
                }
                return BadRequest<string>("Failed To Send Reset Password Code");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(SendResetPasswordByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.IdentityUnitOFWork.UserServices.GetUserByPhoneNumberAsync(request.PhoneNumber);
                //Generate Token For Reset Password
                string CodeOfResetPassword = await UnitOFWork.AuthenticationServices.GenerateResetPasswordCode();
                UserThatWantToResetPassword.ResetPasswordCode = CodeOfResetPassword;
                bool CodeToResetPasswordIsSendSuccessfullyOrNot = await UnitOFWork.ExternalNotificationUnitOFWork.SMSServices.SendSMSAsync($"Your reset password code is: {CodeOfResetPassword}", request.PhoneNumber);
                if (CodeToResetPasswordIsSendSuccessfullyOrNot)
                {
                    //Save Changes In Database
                    await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToResetPassword);
                    return OK<string>("Reset Password Code Sent Successfully");
                }
                return BadRequest<string>("Failed To Send Reset Password Code");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }
    }
}
