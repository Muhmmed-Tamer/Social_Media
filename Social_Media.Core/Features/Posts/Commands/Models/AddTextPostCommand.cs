using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class AddTextPostCommand : IRequest<Response<string>>
    {
        public AddTextPostCommand()
        {

        }
        public AddTextPostCommand(string Post_Content, string UserId_That_Want_To_AddPost, Privacy Post_Privacy)
        {
            this.Post_Content = Post_Content;
            this.UserId_That_Want_To_AddPost = UserId_That_Want_To_AddPost;
            this.Post_Privacy = Post_Privacy;
        }
        public string Post_Content { get; set; }
        public string UserId_That_Want_To_AddPost { get; set; }
        public Privacy Post_Privacy { get; set; }
    }
}
