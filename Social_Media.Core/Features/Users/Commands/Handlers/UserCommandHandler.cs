using ConstantStatementInAllProject.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<ResetPasswordByEmailCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public UserCommandHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser Mapped_User = UnitOFWork.Mapper.Map<ApplicationUser>(request);
                (string, bool) GetPathOFImage = default;
                if (request.User_Picture is not null)
                {
                    GetPathOFImage = await UnitOFWork.FileServices.GeneratePathOFFile(request?.User_Picture,
                    UnitOFWork.ConfigurationOFUserImageServices.MaxSize(), UnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn(),
                    UnitOFWork.ConfigurationOFUserImageServices.AllowedExtension());

                    if (GetPathOFImage.Item1 is not null && GetPathOFImage.Item2 == true)
                    {
                        Mapped_User.PicturePath = GetPathOFImage.Item1;
                    }
                    else if (GetPathOFImage.Item1 == FilesConstants.ErrorSizeFile & GetPathOFImage.Item2 == false)
                    {
                        return BadRequest<string>(GetPathOFImage.Item1 + $"{UnitOFWork.ConfigurationOFUserImageServices.MaxSize()} Mega Byte");
                    }
                    else if (GetPathOFImage.Item1 == FilesConstants.ErrorExtensionFile & GetPathOFImage.Item2 == false)
                    {
                        return BadRequest<string>(GetPathOFImage.Item1 + string.Join(", ", UnitOFWork.ConfigurationOFUserImageServices.AllowedExtension()));
                    }
                }

                IdentityResult Result_OF_CreateUser = await UnitOFWork.UserServices.ManagerUser.CreateAsync(Mapped_User, request.Password);
                if (Result_OF_CreateUser.Succeeded)
                {
                    //Send Email Confirmation Link
                    string ConfirmEmailToken = await UnitOFWork.UserServices.ManagerUser.GenerateEmailConfirmationTokenAsync(Mapped_User);
                    bool EmailIsSentSuccessfully = await UnitOFWork.EmailServices.
                        SendEmailAsync("Confirm Your Email", $"Please confirm your email by clicking <a href='{UnitOFWork.ProtocolAndHostServices.GoToEndPointInMyApplication("Authentication", "ConfirmEmail", new { EmailToken = ConfirmEmailToken, UserId = Mapped_User.Id })}'>here</a>.", Mapped_User.Email);
                    if (EmailIsSentSuccessfully)
                    {
                        return Created<string>("User Created Successfully");
                    }
                    return BadRequest<string>("User Is Created Successfully But Confirmation Email Is Not Send");
                }
                else
                {
                    if (request.User_Picture is not null)
                    {
                        bool FileIsDeleted = await UnitOFWork.FileServices.DeleteFile(GetPathOFImage.Item1, UnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn());
                        if (FileIsDeleted)
                        {
                            return BadRequest<string>("User Is Not Created", Result_OF_CreateUser.Errors.Select(E => E.Description).ToList());
                        }
                    }
                }
                return BadRequest<string>("User Is Not Created", Result_OF_CreateUser.Errors.Select(E => E.Description).ToList());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.Email);
                if (UserThatWantToResetPassword is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                if (UserThatWantToResetPassword.ResetPasswordCode != request.CodeToResetPassword)
                {
                    return BadRequest<string>("Invalid Code");
                }

                await UnitOFWork.UserServices.ManagerUser.RemovePasswordAsync(UserThatWantToResetPassword);
                await UnitOFWork.UserServices.ManagerUser.AddPasswordAsync(UserThatWantToResetPassword, request.NewPassword);
                return OK("Password Is Reset Successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }
    }
}
