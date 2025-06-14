using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Services.ImplementationServices.PostServices
{
    public class ImageOrVideoPostServices : Services<ImageOrVideoPost>, IImageOrVideoPostServices
    {
        public ImageOrVideoPostServices(ILogger<Services<ImageOrVideoPost>> Logger, IRepository<ImageOrVideoPost> Repository) : base(Logger, Repository)
        {
        }
    }
}
