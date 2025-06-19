using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories.StoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.InfraStructure.ImplementationRepositories.StoryRepository
{
    public class ImageOrVideoStoryPathRepository : Repository<ImageOrVideoStoryPath>, IImageOrVideoStoryPathRepository
    {
        public ImageOrVideoStoryPathRepository(Data.ContextData Data, ILogger<Repository<ImageOrVideoStoryPath>> Logger) : base(Data, Logger)
        {
        }
    }
}
