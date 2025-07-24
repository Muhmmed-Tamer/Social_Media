using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Queries.Models;
using Social_Media.Core.Features.Posts.Queries.Results;
using Social_Media.Core.Response_Structure;
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
                List<Post> AllPostsOFUser = await UnitOFWork.PostUnitOFWork.PostServices.GetPostsOFUserAsync(request.UserId);
                List<PostsOFUserQuery> PostsOFUser = UnitOFWork.Mapper.Map<List<PostsOFUserQuery>>(AllPostsOFUser);
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
