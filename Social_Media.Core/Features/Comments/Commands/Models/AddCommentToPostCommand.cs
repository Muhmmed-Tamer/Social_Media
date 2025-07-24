using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Comments.Commands.Models
{
    public class AddCommentToPostCommand : IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string Comment_Content { get; set; }
        public string UserId { get; set; }
        public AddCommentToPostCommand()
        {

        }
        public AddCommentToPostCommand(int PostId, string Comment_Content, string UserId)
        {
            this.Comment_Content = Comment_Content;
            this.PostId = PostId;
            this.UserId = UserId;
        }
    }
}
