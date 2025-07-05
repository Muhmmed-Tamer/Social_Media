using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Models
{
    public class DeleteInteractionWithCommentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int CommentId { get; set; }

        public DeleteInteractionWithCommentCommand() { }
        public DeleteInteractionWithCommentCommand(int id, int commentId)
        {
            Id = id;
            CommentId = commentId;
        }

    }
}
