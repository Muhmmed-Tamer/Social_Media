using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Comments.Commands.Models
{
    public class AddReplyOFCommentCommand : IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public string UserIdThatWriteAComment { get; set; }
        public string UserIdThatWriteAReplyOFComment { get; set; }
        public string Content { get; set; }
        public AddReplyOFCommentCommand() { }

        public AddReplyOFCommentCommand(int CommentId, string Content, string UserIdThatWriteAComment, string UserIdThatWriteAReplyOFComment)
        {
            this.CommentId = CommentId;
            this.UserIdThatWriteAComment = UserIdThatWriteAComment;
            this.UserIdThatWriteAReplyOFComment = UserIdThatWriteAReplyOFComment;
            this.Content = Content;
        }
    }
}
