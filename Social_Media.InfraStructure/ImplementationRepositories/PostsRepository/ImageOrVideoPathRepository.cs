using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.PostsRepository
{
    public class ImageOrVideoPathRepository : Repository<ImageOrVideoPath>, IImageOrVideoPathRepository
    {
        public ImageOrVideoPathRepository(Data.ContextData Data, ILogger<Repository<ImageOrVideoPath>> Logger) : base(Data, Logger)
        {
        }
    }
}
