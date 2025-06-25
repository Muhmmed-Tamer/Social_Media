using Serilog;
using Social_Media.Data.Models.Story;

namespace Social_Media.InfraStructure.ImplementationRepositories.StoryRepository
{
    public class StoryRepository : Repository<Story>
    {
        public StoryRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
        }
    }
}
