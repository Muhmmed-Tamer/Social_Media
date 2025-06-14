using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Core.Response_Structure;
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
            try
            {

                var ImageExtensions = UnitOFWork.Configuration.GetSection("Posts:Images:AllowedExtension").Get<string[]>();
                var VideoExtensions = UnitOFWork.Configuration.GetSection("Posts:Videos:AllowedExtension").Get<string[]>();

                var FileExtension = Path.GetExtension(request.ImageOrVideo.FileName).ToLowerInvariant();

                (string Path, bool IsValid) FilePathResult;
                long MaxSizeBytes;
                string SizeErrorMessage;

                if (ImageExtensions.Contains(FileExtension))
                {
                    MaxSizeBytes = UnitOFWork.Configuration.GetSection("Posts:Images:MaxSize").Get<long>();
                    FilePathResult = await UnitOFWork.FileServices.GeneratePathOFFile(request.ImageOrVideo, MaxSizeBytes, UnitOFWork.Configuration.GetSection("Posts:Images:DirectoryThatStoreFileIn").Get<string>()!, ImageExtensions);
                    SizeErrorMessage = $"The Max Size Of Image Is [{MaxSizeBytes / (1024 * 1024)}] Mega byte";
                }
                else if (VideoExtensions.Contains(FileExtension))
                {
                    MaxSizeBytes = UnitOFWork.Configuration.GetSection("Posts:Videos:MaxSize").Get<long>();
                    FilePathResult = await UnitOFWork.FileServices.GeneratePathOFFile(request.ImageOrVideo, MaxSizeBytes, UnitOFWork.Configuration.GetSection("Posts:Videos:DirectoryThatStoreFileIn").Get<string>()!, VideoExtensions);
                    SizeErrorMessage = $"The Max Size Of Video Is [{MaxSizeBytes / (1024 * 1024 * 1024)}] Giga byte";
                }
                else
                {
                    return BadRequest<string>("Invalid file type. Allowed extensions: " + string.Join(", ", ImageExtensions.Concat(VideoExtensions)));
                }

                ImageOrVideoPost MappedPost = UnitOFWork.Mapper.Map<ImageOrVideoPost>(request);

                if (FilePathResult.Path is not null & FilePathResult.IsValid)
                {
                    MappedPost.ImageOrVideoPath = FilePathResult.Path;
                }
                else if (FilePathResult.Path == string.Empty & !FilePathResult.IsValid)
                {
                    return BadRequest<string>(SizeErrorMessage);
                }
                else
                {
                    return BadRequest<string>("Some Thing Is Wrong When Adding Post");
                }
                await UnitOFWork.ImageOrVideoPostServices.AddAsync(MappedPost);
                await UnitOFWork.ImageOrVideoPostServices.SaveChangesAsync();

                return Created<string>("Post created successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating media post");
                return BadRequest<string>("An error occurred while creating the post");
            }
        }
    }
}
