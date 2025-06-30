using MediatR;
using Social_Media.Core.Features.Posts.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Posts.Queries.Models
{
    public class GetPostsOFUserQuery : IRequest<Response<List<PostsOFUserQuery>>>
    {
        public string UserId { get; set; }
        public GetPostsOFUserQuery()
        {

        }
        public GetPostsOFUserQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
