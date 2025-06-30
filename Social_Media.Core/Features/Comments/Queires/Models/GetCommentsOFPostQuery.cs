using MediatR;
using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Comments.Queires.Models
{
    public class GetCommentsOFPostQuery : IRequest<Response<List<CommentsOFPostQuery>>>
    {
        public int PostId { get; set; }
        public GetCommentsOFPostQuery()
        {

        }
        public GetCommentsOFPostQuery(int postId)
        {
            PostId = postId;
        }
    }
}
