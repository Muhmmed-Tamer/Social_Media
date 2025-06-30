using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class AddImageOrVideoPostCommand : IRequest<Response<string>>
    {
        public AddImageOrVideoPostCommand()
        {

        }
        public AddImageOrVideoPostCommand(string? Post_Title, string UserId_That_Want_To_AddPost, Privacy Post_Privacy, List<IFormFile> ImageOrVideos)
        {
            this.Post_Title = Post_Title;
            this.UserId_That_Want_To_AddPost = UserId_That_Want_To_AddPost;
            this.Post_Privacy = Post_Privacy;
            this.ImageOrVideos = ImageOrVideos;
        }
        public string? Post_Title { get; set; }
        public string UserId_That_Want_To_AddPost { get; set; }
        public Privacy Post_Privacy { get; set; }
        public List<IFormFile> ImageOrVideos { get; set; }
    }
}
