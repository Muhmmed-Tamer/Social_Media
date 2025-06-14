using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;
using Social_Media.Models;

namespace Social_Media.InfraStructure.ImplementationRepositories.PostsRepository
{
    public class ImageOrVideoRepository : Repository<ImageOrVideoPost>, IImageOrVideoPostRepository
    {
        public ImageOrVideoRepository(Data.ContextData Data, ILogger<Repository<ImageOrVideoPost>> Logger) : base(Data, Logger)
        {
        }
    }
}
