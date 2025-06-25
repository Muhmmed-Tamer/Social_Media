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
                ApplicationUser UserThatWantToConfirmEmail = await UnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                if (UserThatWantToConfirmEmail is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                IdentityResult EmailIsConfirmedSuccessfully = await UnitOFWork.UserServices.ManagerUser.ConfirmEmailAsync(UserThatWantToConfirmEmail, request.Token);
                if (EmailIsConfirmedSuccessfully.Succeeded)
                {
                    return OK<string>("Email Confirmed Successfully");
                }
                else
                {
                    return BadRequest<string>("Invalid Or Expired Token");
                }
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
                //Check User Is Exist Or Not
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                if (UserThatWantToResetPassword is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                //Generate Token For Reset Password
                string CodeOfResetPassword = await UnitOFWork.AuthenticationServices.GenerateResetPasswordCode();
                UserThatWantToResetPassword.ResetPasswordCode = CodeOfResetPassword;
                bool CodeToResetPasswordIsSendSuccessfullyOrNot = await UnitOFWork.EmailServices.SendEmailAsync("Reset Password", $"Your reset password code is: {CodeOfResetPassword}", UserThatWantToResetPassword.Email);
                if (CodeToResetPasswordIsSendSuccessfullyOrNot)
                {
                    //Save Changes In Database
                    await UnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToResetPassword);
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

        public async Task<Response<string>> Handle(SendResetPasswordByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.UserServices.GetUserByPhoneNumberAsync(request.PhoneNumber);
                if (UserThatWantToResetPassword is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                //Generate Token For Reset Password
                string CodeOfResetPassword = await UnitOFWork.AuthenticationServices.GenerateResetPasswordCode();
                UserThatWantToResetPassword.ResetPasswordCode = CodeOfResetPassword;
                bool CodeToResetPasswordIsSendSuccessfullyOrNot = await UnitOFWork.SMSServices.SendSMSAsync($"Your reset password code is: {CodeOfResetPassword}", request.PhoneNumber);
                if (CodeToResetPasswordIsSendSuccessfullyOrNot)
                {
                    //Save Changes In Database
                    await UnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToResetPassword);
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
