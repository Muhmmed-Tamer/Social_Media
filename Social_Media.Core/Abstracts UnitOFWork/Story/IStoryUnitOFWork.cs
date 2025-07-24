using Social_Media.Services.AbstractsServices.StoryServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IStoryUnitOFWork
    {
        IImageOrVideoStoryPathService ImageOrVideoStoryPathService { get; }
        IStoryService StoryService { get; }
    }
}
