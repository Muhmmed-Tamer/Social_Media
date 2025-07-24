using ConstantStatementInAllProject.Files;
using MediatR;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Story.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Story;

namespace Social_Media.Core.Features.Story.Commands.Handlers
{
    internal class StoryCommandHandler : ResponseHandler, IRequestHandler<AddStoryCommand, Response<string>>
    {

        private readonly Serilog.ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;

        public StoryCommandHandler(Serilog.ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddStoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Data.Models.Story.Story story = UnitOFWork.Mapper.Map<Social_Media.Data.Models.Story.Story>(command);
                await UnitOFWork.StoryUnitOFWork.StoryService.AddAsync(story);
                await UnitOFWork.StoryUnitOFWork.StoryService.SaveChangesAsync();



                return Created<string>("Story has been added successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error while adding story");
                return BadRequest<string>("An error occurred.");
            }

        }
        public async Task<Response<string>> Handle(AddImageOrVideoStoryCommand command, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.StoryUnitOFWork.ImageOrVideoStoryPathService.BeginTransaction())

            {
                try
                {
                    Data.Models.Story.Story story = UnitOFWork.Mapper.Map<Data.Models.Story.Story>(command);
                    await UnitOFWork.StoryUnitOFWork.StoryService.AddAsync(story);
                    await UnitOFWork.StoryUnitOFWork.StoryService.SaveChangesAsync();


                    (string, bool) PathOfImageOrVideoStory = (null, false);
                    if (command.StoryType == StoryType.Video)
                    {
                        PathOfImageOrVideoStory = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFile(command.ImageOrVideo, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());
                    }
                    else if (command.StoryType == StoryType.Image)
                    {
                        PathOfImageOrVideoStory = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFile(command.ImageOrVideo, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.AllowedExtension());
                    }
                    if (PathOfImageOrVideoStory.Item1.Contains(FilesConstants.ErrorExtensionFiles) || PathOfImageOrVideoStory.Item1.Contains(FilesConstants.ErrorSizeFiles) || PathOfImageOrVideoStory.Item2 == false)
                    {
                        await UnitOFWork.StoryUnitOFWork.ImageOrVideoStoryPathService.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathOfImageOrVideoStory.Item1.ToString());

                    }
                    ImageOrVideoStoryPath imageOrVideoStoryPath = new ImageOrVideoStoryPath()
                    {
                        StoryId = story.Id,
                        Image_Or_VideoPath = PathOfImageOrVideoStory.Item1

                    };
                    await UnitOFWork.StoryUnitOFWork.ImageOrVideoStoryPathService.AddAsync(imageOrVideoStoryPath);
                    await UnitOFWork.StoryUnitOFWork.StoryService.SaveChangesAsync();
                    await UnitOFWork.StoryUnitOFWork.StoryService.CommitTransaction(Transaction);
                    return Created<string>("Story Is Created Successfully");


                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Error while adding story");
                    return BadRequest<string>("An error occurred.");

                }

            }
        }
    }
}
