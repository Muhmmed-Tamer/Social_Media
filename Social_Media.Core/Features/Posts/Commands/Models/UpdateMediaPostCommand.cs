using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class UpdateMediaPostCommand : IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string Caption { get; set; }
        public string UserId { get; set; }
        public List<IFormFile> Media { get; set; }
        public UpdateMediaPostCommand() { }
        public UpdateMediaPostCommand(int PostId, string Caption, string UserId, List<IFormFile> Media)
        {
            this.PostId = PostId;
            this.Caption = Caption;
            this.UserId = UserId;
            this.Media = Media;
        }
    }
}
