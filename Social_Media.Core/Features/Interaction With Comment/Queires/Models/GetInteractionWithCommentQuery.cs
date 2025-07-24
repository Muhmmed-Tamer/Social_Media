using MediatR;
using Social_Media.Core.Features.Interaction_With_Comment.Queires.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Interaction_With_Comment.Queires.Models
{
    public class GetInteractionWithCommentQuery : IRequest<Response<List<InteractionWithCommentQuery>>>
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public GetInteractionWithCommentQuery() { }
        public GetInteractionWithCommentQuery(int PostId, int CommentId)
        {
            this.CommentId = CommentId;
            this.PostId = PostId;
        }
    }
}
