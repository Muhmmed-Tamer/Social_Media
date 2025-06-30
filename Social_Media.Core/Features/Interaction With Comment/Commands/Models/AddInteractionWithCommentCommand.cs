using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Models
{
    public class AddInteractionWithCommentCommand : IRequest<Response<string>>
    {
        public InteractionType InteractBy { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public AddInteractionWithCommentCommand()
        {

        }
        public AddInteractionWithCommentCommand(string UserId, int CommentId, int PostId, InteractionType InteractBy)
        {
            this.InteractBy = InteractBy;
            this.CommentId = CommentId;
            this.UserId = UserId;
            this.PostId = PostId;
        }
    }
}
