using Serilog;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.StoryServices;

namespace Social_Media.Services.ImplementationServices.StoryServices
{
    public class ImageOrVideoStoryPathService : Services<ImageOrVideoStoryPath>, IImageOrVideoStoryPathService
    {
        public ImageOrVideoStoryPathService(ILogger Logger, IRepository<ImageOrVideoStoryPath> Repository) : base(Logger, Repository)
        {
        }
    }
}
