using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Services.ImplementationServices.PostServices
{
    public class ImageOrVideoPathServices : Services<ImageOrVideoPath>, IImageOrVideoPathServices
    {
        public ImageOrVideoPathServices(ILogger<Services<ImageOrVideoPath>> Logger, IRepository<ImageOrVideoPath> Repository) : base(Logger, Repository)
        {
        }
    }
}
