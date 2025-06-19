using ConstantStatementInAllProject.Files;
using MediatR;
using Microsoft.Extensions.Logging;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Handlers;
using Social_Media.Core.Features.Story.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Story;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Story.Commands.Handlers
{
    internal class StoryCommandHandler : ResponseHandler, IRequestHandler<AddStoryCommand, Response<string>>
    {

        private readonly ILogger<PostCommandHandler> Logger;
        private readonly IUnitOFWork UnitOFWork;

        public StoryCommandHandler(ILogger<PostCommandHandler> Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddStoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Data.Models.Story.Story story = UnitOFWork.Mapper.Map<Social_Media.Data.Models.Story.Story>(command);
                await UnitOFWork.StoryServices.AddAsync(story);
                await UnitOFWork.StoryServices.SaveChangesAsync();
               
               

                return Created<string>("Story has been added successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while adding story");
                return BadRequest<string>("An error occurred.");
            }

        }
        public async Task<Response<string>> Handle(AddImageOrVideoStoryCommand command, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.ImageOrVideoStoryPathService.BeginTransaction())

            {
                try
                {
                    Data.Models.Story.Story story = UnitOFWork.Mapper.Map<Data.Models.Story.Story>(command);
                    await UnitOFWork.StoryServices.AddAsync(story);
                    await UnitOFWork.StoryServices.SaveChangesAsync();

                
                    (string, bool) PathOfImageOrVideoStory = (null, false);
                    if (command.StoryType == StoryType.Video)
                    {
                        PathOfImageOrVideoStory = await UnitOFWork.FileServices.GeneratePathOFFile(command.ImageOrVideo, UnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());
                    }
                    else if (command.StoryType == StoryType.Image)
                    {
                        PathOfImageOrVideoStory = await UnitOFWork.FileServices.GeneratePathOFFile(command.ImageOrVideo, UnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOFPostImageServices.AllowedExtension());
                    }
                    if (PathOfImageOrVideoStory.Item1.Contains(FilesConstants.ErrorExtensionFiles) || PathOfImageOrVideoStory.Item1.Contains(FilesConstants.ErrorSizeFiles) || PathOfImageOrVideoStory.Item2 == false)
                    {
                        await UnitOFWork.ImageOrVideoStoryPathService.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathOfImageOrVideoStory.Item1.ToString());

                    }
                    ImageOrVideoStoryPath imageOrVideoStoryPath = new ImageOrVideoStoryPath()
                    {
                        StoryId = story.Id,
                        Image_Or_VideoPath = PathOfImageOrVideoStory.Item1

                    };
                   await UnitOFWork.ImageOrVideoStoryPathService.AddAsync(imageOrVideoStoryPath);
                   await UnitOFWork.ImageOrVideoStoryPathService.SaveChangesAsync();    
                   await UnitOFWork.ImageOrVideoStoryPathService.CommitTransaction(Transaction);
                   return Created<string>("Story Is Created Successfully");


                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Error while adding story");
                    return BadRequest<string>("An error occurred.");

                }

            }
        }
    }
    }
