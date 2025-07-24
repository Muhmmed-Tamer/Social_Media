using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Roles;
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
        IRequestHandler<ResetPasswordByEmailCommand, Response<string>>, IRequestHandler<DeleteUserCommand, Response<string>>,
         IRequestHandler<UpdateUserPhotoCommand, Response<string>>,
        IRequestHandler<UpdateUserNameCommand, Response<string>>,
          IRequestHandler<UpdateUserDescriptionCommand, Response<string>>

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
            (string PathOFImageProfile, bool IsStoredSuccessfully) = (string.Empty, false);
            try
            {
                ApplicationUser Mapped_User = UnitOFWork.Mapper.Map<ApplicationUser>(request);
                if (request.User_Picture is not null)
                {
                    (PathOFImageProfile, IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFile(request?.User_Picture,
                    UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn(),
                    UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.AllowedExtension());

                    if (PathOFImageProfile == FilesConstants.ErrorSizeFile && !IsStoredSuccessfully)
                    {
                        return BadRequest<string>(FilesConstants.ErrorSizeFile + $"{UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.MaxSize()} Mega Byte");
                    }
                    else if (PathOFImageProfile == FilesConstants.ErrorExtensionFile && !IsStoredSuccessfully)
                    {
                        return BadRequest<string>(FilesConstants.ErrorExtensionFile + string.Join(", ", UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.AllowedExtension()));
                    }
                    Mapped_User.PicturePath = PathOFImageProfile;
                }

                IdentityResult Result_OF_CreateUser = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.CreateAsync(Mapped_User, request.Password);
                IdentityResult ResultOFAddRoleToCurrentUser = await AddRegularUserRoleToUser(Mapped_User);
                if (Result_OF_CreateUser.Succeeded)
                {
                    bool EmailIsSentSuccessfully = await ResultOFEmailNotification(Mapped_User);
                    if (!ResultOFAddRoleToCurrentUser.Succeeded)
                    {
                        return BadRequest<string>("User Is Created Successfully But Not Add Role");
                    }
                    //Send Email Confirmation Link
                    if (!EmailIsSentSuccessfully)
                    {
                        return BadRequest<string>("User Is Created Successfully But Confirmation Email Is Not Send");
                    }
                    return Created<string>("User Created Successfully");
                }
                else
                {
                    if (request.User_Picture is not null)
                    {
                        bool FileIsDeleted = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFile(PathOFImageProfile, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn());
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
                await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFile(PathOFImageProfile, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn());
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }
        public async Task<Response<string>> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserThatWantToResetPassword = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.Email);
                if (UserThatWantToResetPassword is null)
                {
                    return BadRequest<string>("User Not Found");
                }
                if (UserThatWantToResetPassword.ResetPasswordCode != request.CodeToResetPassword)
                {
                    return BadRequest<string>("Invalid Code");
                }

                await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.RemovePasswordAsync(UserThatWantToResetPassword);
                await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.AddPasswordAsync(UserThatWantToResetPassword, request.NewPassword);
                return OK("Password Is Reset Successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        private async Task<bool> ResultOFEmailNotification(ApplicationUser Mapped_User)
        {
            try
            {
                string ConfirmEmailToken = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.GenerateEmailConfirmationTokenAsync(Mapped_User);
                bool EmailIsSentSuccessfully = await UnitOFWork.ExternalNotificationUnitOFWork.EmailServices.
                    SendEmailAsync("Confirm Your Email", $"Please confirm your email by clicking <a href='{UnitOFWork.ProtocolAndHostServices.GoToEndPointInMyApplication("Authentication", "ConfirmEmail", new { EmailToken = ConfirmEmailToken, UserId = Mapped_User.Id })}'>here</a>.", Mapped_User.Email);
                return EmailIsSentSuccessfully;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }


        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser applicationUser = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                if (applicationUser is null) return BadRequest<string>("User Not Fount");

                applicationUser.IsDeleted = true;
                await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(applicationUser);
                return OK("Within 90 days, if you do not log in, the account will be permanently deleted.");

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<string>(ex.Message);

            }
        }
        private async Task<IdentityResult> AddRegularUserRoleToUser(ApplicationUser Mapped_User)
        {
            try
            {
                return await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.AddToRoleAsync(Mapped_User, RolesConstants.User);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }
        public async Task<Response<string>> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserThatWantToUpdateName = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToUpdateName);
                return OK("Updated Successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }
        public async Task<Response<string>> Handle(UpdateUserPhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserThatWantToUpdatePhoto = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                (string Path, bool IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFile(request.Photo, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.AllowedExtension());


                if (Path == FilesConstants.ErrorSizeFile && !IsStoredSuccessfully)
                {
                    return BadRequest<string>(FilesConstants.ErrorSizeFile + $"{UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.MaxSize()} Mega Byte");
                }
                else if (Path == FilesConstants.ErrorExtensionFile && !IsStoredSuccessfully)
                {
                    return BadRequest<string>(FilesConstants.ErrorExtensionFile + string.Join(", ", UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.AllowedExtension()));
                }
                else
                {
                    // Delete the old picture if it exists
                    if (!string.IsNullOrEmpty(UserThatWantToUpdatePhoto.PicturePath))
                    {
                        bool FileIsDeleted = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFile(UserThatWantToUpdatePhoto.PicturePath, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFUserImageServices.DirectoryThatStoreFileIn());
                        if (!FileIsDeleted)
                        {
                            return BadRequest<string>("Failed to delete the old picture.");
                        }
                    }
                    UserThatWantToUpdatePhoto.PicturePath = Path;
                    await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToUpdatePhoto);
                    return OK("Updated Successfully");
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return BadRequest<string>(ex.Message);

            }
        }

        public async Task<Response<string>> Handle(UpdateUserDescriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var UserThatWantToUpdatePhoto = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                UserThatWantToUpdatePhoto.DescriptionOFProfile = request.Description;
                await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.UpdateAsync(UserThatWantToUpdatePhoto);
                return OK("Updated Successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return BadRequest<string>(ex.Message);
            }

        }

    }

}
