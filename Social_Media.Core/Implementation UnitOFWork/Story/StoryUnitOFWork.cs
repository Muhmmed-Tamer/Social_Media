using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.StoryServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class StoryUnitOFWork : IStoryUnitOFWork
    {
        public StoryUnitOFWork(IImageOrVideoStoryPathService ImageOrVideoStoryPathService, IStoryService StoryService)
        {
            this.ImageOrVideoStoryPathService = ImageOrVideoStoryPathService;
            this.StoryService = StoryService;
        }

        public IImageOrVideoStoryPathService ImageOrVideoStoryPathService { get; }

        public IStoryService StoryService { get; }
    }
}
