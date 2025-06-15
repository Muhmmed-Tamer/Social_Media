using ConstantStatementInAllProject.Files;
using MediatR;
using Microsoft.Extensions.Logging;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Posts;
using Social_Media.Models;

namespace Social_Media.Core.Features.Posts.Commands.Handlers
{
    public class PostCommandHandler : ResponseHandler, IRequestHandler<AddTextPostCommand, Response<string>>,
        IRequestHandler<AddImageOrVideoPostCommand, Response<string>>
    {
        private readonly ILogger<PostCommandHandler> Logger;
        private readonly IUnitOFWork UnitOFWork;
        public PostCommandHandler(ILogger<PostCommandHandler> Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddTextPostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                TextPost Mapped_TextPost = UnitOFWork.Mapper.Map<TextPost>(request);
                await UnitOFWork.TextPostServices.AddAsync(Mapped_TextPost);
                await UnitOFWork.TextPostServices.SaveChangesAsync();
                return Created<string>("Post Is Created Successfully");
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return BadRequest<string>(ex.Message);
            }
        }

        #region Old Method
        //public async Task<Response<string>> Handle(AddImageOrVideoPostCommand request, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        (string, bool) Get_Path_Of_ImageOrVideo = default;
        //        long MaxSize_MustBe_as = default;
        //        //User Want To Add Post As An Image
        //        if (UnitOFWork.Configuration.GetValue<string[]>("Posts:0:Images:AllowedExtension")!.Contains(Path.GetExtension(request.ImageOrVideo.FileName)))
        //        {
        //            Get_Path_Of_ImageOrVideo = await UnitOFWork.FileServices.GeneratePathOFFile(request.ImageOrVideo, UnitOFWork.Configuration.GetValue<long>("Posts:0:Images:MaxSize"),
        //                UnitOFWork.Configuration.GetValue<string>("Posts:0:Images:DirectoryThatStoreFileIn")!, UnitOFWork.Configuration.GetValue<string[]>("Posts:0:Images:AllowedExtension")!);
        //            MaxSize_MustBe_as = UnitOFWork.Configuration.GetValue<long>("Posts:0:Images:MaxSize");
        //        }
        //        else
        //        {
        //            Get_Path_Of_ImageOrVideo = await UnitOFWork.FileServices.GeneratePathOFFile(request.ImageOrVideo, UnitOFWork.Configuration.GetValue<long>("Posts:0:Videos:MaxSize"),
        //                UnitOFWork.Configuration.GetValue<string>("Posts:0:Videos:DirectoryThatStoreFileIn")!, UnitOFWork.Configuration.GetValue<string[]>("Posts:0:Videos:AllowedExtension")!);
        //            MaxSize_MustBe_as = UnitOFWork.Configuration.GetValue<long>("Posts:0:Videos:MaxSize");
        //        }
        //        ImageOrVideoPost Mapped_ImageOrVideoPost = UnitOFWork.Mapper.Map<ImageOrVideoPost>(request);
        //        if (Get_Path_Of_ImageOrVideo.Item1 is not null && Get_Path_Of_ImageOrVideo.Item2 == true)
        //        {
        //            Mapped_ImageOrVideoPost.ImageOrVideoPath = Get_Path_Of_ImageOrVideo.Item1;
        //        }
        //        else if (Get_Path_Of_ImageOrVideo.Item1 == string.Empty && Get_Path_Of_ImageOrVideo.Item2 == false)
        //        {
        //            return BadRequest<string>($"The Max Size OF Post Is [{MaxSize_MustBe_as / (1024 * 1024)}] Mega Byte");
        //        }
        //        else
        //        {
        //            return BadRequest<string>($"Some Thing Is Wrong When I Add Post");
        //        }
        //        await UnitOFWork.ImageOrVideoPostServices.AddAsync(Mapped_ImageOrVideoPost);
        //        await UnitOFWork.ImageOrVideoPostServices.SaveChangesAsync();
        //        return Created<string>("Post Is Created Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogWarning(ex.Message);
        //        return BadRequest<string>(ex.Message);
        //    }
        //} 
        #endregion
        public async Task<Response<string>> Handle(AddImageOrVideoPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.ImageOrVideoPostServices.BeginTransaction())
            {
                try
                {
                    ImageOrVideoPost Mapped_ImageOrVideoPost = UnitOFWork.Mapper.Map<ImageOrVideoPost>(request);
                    await UnitOFWork.ImageOrVideoPostServices.AddAsync(Mapped_ImageOrVideoPost);
                    await UnitOFWork.ImageOrVideoPostServices.SaveChangesAsync();


                    (List<string>, bool) PathsOFImagesOrVideos = await UnitOFWork.FileServices.GeneratePathOFFiles(request.ImageOrVideos, UnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOFPostImageServices.AllowedExtension(),
                        UnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(),
                        UnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());

                    if (PathsOFImagesOrVideos.Item1.Contains(FilesConstants.ErrorExtensionFiles) || PathsOFImagesOrVideos.Item1.Contains(FilesConstants.ErrorSizeFiles) && PathsOFImagesOrVideos.Item2 == false)
                    {
                        await UnitOFWork.ImageOrVideoPostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Item1.Select(E => E).ToString()!);
                    }
                    else if (PathsOFImagesOrVideos.Item2 == false)
                    {
                        await UnitOFWork.ImageOrVideoPostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Item1.Select(E => E).ToString()!);
                    }

                    foreach (var Path in PathsOFImagesOrVideos.Item1)
                    {
                        ImageOrVideoPath imageOrVideoPath = new ImageOrVideoPath()
                        {
                            PostId = Mapped_ImageOrVideoPost.Id,
                            Image_Or_VideoPath = Path
                        };

                        await UnitOFWork.ImageOrVideoPathServices.AddAsync(imageOrVideoPath);
                    }
                    await UnitOFWork.ImageOrVideoPathServices.SaveChangesAsync();
                    await UnitOFWork.ImageOrVideoPostServices.CommitTransaction(Transaction);
                    return Created<string>("Post Is Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.ImageOrVideoPostServices.RollbackTransaction(Transaction);
                    Logger.LogError(ex, "Error creating media post");
                    return BadRequest<string>("An error occurred while creating the post");
                }
            }
        }
    }
}
