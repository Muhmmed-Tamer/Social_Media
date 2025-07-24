using Serilog;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories.StoryRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.StoryRepository
{
    public class ImageOrVideoStoryPathRepository : Repository<ImageOrVideoStoryPath>, IImageOrVideoStoryPathRepository
    {
        public ImageOrVideoStoryPathRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
        }
    }
}
