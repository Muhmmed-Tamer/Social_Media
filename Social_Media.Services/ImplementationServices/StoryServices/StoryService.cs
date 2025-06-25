using Serilog;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.StoryServices;

namespace Social_Media.Services.ImplementationServices.StoryServices
{
    public class StoryService : Services<Story>, IStoryService
    {
        public StoryService(ILogger Logger, IRepository<Story> Repository) : base(Logger, Repository)
        {
        }
    }
}
