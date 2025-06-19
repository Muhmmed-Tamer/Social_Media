using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.InfraStructure.ImplementationRepositories.StoryRepository
{
    public class StoryRepository : Repository<Story>
    {
        public StoryRepository(Data.ContextData Data, ILogger<Repository<Story>> Logger) : base(Data, Logger)
        {
        }
    }
}
