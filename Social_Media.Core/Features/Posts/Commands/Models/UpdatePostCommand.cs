using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class UpdatePostCommand : IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string New_Content { get; set; }
        public string UserId { get; set; }

        public UpdatePostCommand() { }
        public UpdatePostCommand(int PostId, string New_Content, string UserId)
        {
            this.PostId = PostId;
            this.New_Content = New_Content;
            this.UserId = UserId;
        }
    }
}
