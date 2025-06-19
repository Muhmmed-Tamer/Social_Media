using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.StoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Services.ImplementationServices.StoryServices
{
    public class StoryService : Services<Story>, IStoryService
    {
        public StoryService(ILogger<Services<Story>> Logger, IRepository<Story> Repository) : base(Logger, Repository)
        {
        }
    }
}
