using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Queries.Models;
using Social_Media.Core.Features.Posts.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Features.Posts.Queries.Handlers
{
    public class PostsQueryHandler : ResponseHandler, IRequestHandler<GetPostsOFUserQuery, Response<List<PostsOFUserQuery>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public PostsQueryHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<List<PostsOFUserQuery>>> Handle(GetPostsOFUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser UserIsFoundOrNot = await UnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.UserId);
                if (UserIsFoundOrNot == null)
                {
                    Logger.Error($"User with ID {request.UserId} not found.");
                    return BadRequest<List<PostsOFUserQuery>>($"User with ID {request.UserId} not found.");
                }
                List<Post> AllPostsOFUser = await UnitOFWork.PostServices.GetPostsOFUserAsync(request.UserId);
                List<PostsOFUserQuery> PostsOFUser = UnitOFWork.Mapper.Map<List<PostsOFUserQuery>>(AllPostsOFUser);
                var Data = UnitOFWork.ProtocolAndHostServices.GetProtocolAndHost();
                return OK(PostsOFUser);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<List<PostsOFUserQuery>>(ex.Message);
            }
        }
    }
}
