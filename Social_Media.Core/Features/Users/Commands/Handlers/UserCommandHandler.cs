using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly ILogger<UserCommandHandler> Logger;
        private readonly IUnitOFWork UnitOFWork;
        public UserCommandHandler(ILogger<UserCommandHandler> Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                (string, bool) GetPathOFImage = await UnitOFWork.FileServices.GeneratePathOFFile(request.User_Picture, UnitOFWork.Configuration.GetSection("Users:ImageOFProfile:MaxSize").Get<long>(),
                    UnitOFWork.Configuration.GetSection("Users:ImageOFProfile:DirectoryThatStoreFileIn").Get<string>()!, UnitOFWork.Configuration.GetSection("Users:ImageOFProfile:AllowedExtension").Get<string[]>()!);

                ApplicationUser Mapped_User = UnitOFWork.Mapper.Map<ApplicationUser>(request);
                if (GetPathOFImage.Item1 is not null && GetPathOFImage.Item2 == true)
                {
                    Mapped_User.PicturePath = GetPathOFImage.Item1;
                }
                else if (GetPathOFImage.Item1 == string.Empty & GetPathOFImage.Item2 == false)
                {
                    return BadRequest<string>($"The Max Size OF Image Is [{UnitOFWork.Configuration.GetSection("Users:ImageOFProfile:MaxSize").Get<long>() / (1024 * 1024)}] Mega Byte");
                }
                else
                {
                    return BadRequest<string>($"Some Thing Is Wrong When I Add Image");
                }
                IdentityResult Result_OF_CreateUser = await UnitOFWork.ManagerUser.CreateAsync(Mapped_User, request.Password);
                if (Result_OF_CreateUser.Succeeded)
                {
                    return Created<string>("User Created Successfully");
                }
                else
                {
                    bool FileIsDeleted = await UnitOFWork.FileServices.DeleteFile(GetPathOFImage.Item1, UnitOFWork.Configuration.GetSection("Users:ImageOFProfile:DirectoryThatStoreFileIn").Get<string>()!);
                    if (FileIsDeleted)
                    {
                        return BadRequest<string>("User Is Not Created", Result_OF_CreateUser.Errors.Select(E => E.Description).ToList());
                    }
                }
                return BadRequest<string>("User Is Not Created", Result_OF_CreateUser.Errors.Select(E => E.Description).ToList());
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }
    }
}
