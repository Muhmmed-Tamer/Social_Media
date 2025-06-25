using MediatR;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Queries.Models;
using Social_Media.Core.Features.Users.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Features.Users.Queries.Handlers
{
    public class GetUserQueryHandler : ResponseHandler, IRequestHandler<GetUserByEmailQuery, Response<GetUser>>,
        IRequestHandler<GetUserByUserId, Response<GetUser>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly Serilog.ILogger Logger;
        public GetUserQueryHandler(IUnitOFWork UnitOFWork, Serilog.ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<GetUser>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser User = await UnitOFWork.UserServices.ManagerUser.FindByEmailAsync(request.User_Email);
                if (User is null)
                {
                    return BadRequest<GetUser>("User Not Found");
                }
                GetUser Mapped_User = UnitOFWork.Mapper.Map<GetUser>(User);
                Mapped_User.ProfilePictureUrl = UnitOFWork.ProtocolAndHostServices.GetFullPathOFFile(BaseDirectory: "Users", SubDirectory: "ImageOFProfile", FileName: User.PicturePath);
                return OK(Mapped_User);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<GetUser>(ex.Message);
            }
        }

        public async Task<Response<GetUser>> Handle(GetUserByUserId request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser User = await UnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                if (User is null)
                {
                    return BadRequest<GetUser>("User Not Found");
                }
                GetUser Mapped_User = UnitOFWork.Mapper.Map<GetUser>(User);
                Mapped_User.ProfilePictureUrl = UnitOFWork.ProtocolAndHostServices.GetFullPathOFFile(BaseDirectory: "Users", SubDirectory: "ImageOFProfile", FileName: User.PicturePath);
                return OK(Mapped_User);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<GetUser>(ex.Message);
            }
        }
    }
}
