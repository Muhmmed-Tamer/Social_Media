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
    public class ImageOrVideoStoryPathService : Services<ImageOrVideoStoryPath>, IImageOrVideoStoryPathService
    {
        public ImageOrVideoStoryPathService(ILogger<Services<ImageOrVideoStoryPath>> Logger, IRepository<ImageOrVideoStoryPath> Repository) : base(Logger, Repository)
        {
        }
    }
}
