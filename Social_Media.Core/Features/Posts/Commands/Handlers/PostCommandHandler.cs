using ConstantStatementInAllProject.Files;
using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Core.Features.Posts.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Features.Posts.Commands.Handlers
{
    public class PostCommandHandler : ResponseHandler, IRequestHandler<AddTextPostCommand, Response<string>>,
        IRequestHandler<AddImageOrVideoPostCommand, Response<string>>,
        IRequestHandler<DeletePostCommand, Response<string>>, IRequestHandler<UpdatePostCommand, Response<string>>,
        IRequestHandler<UpdateMediaPostCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public PostCommandHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddTextPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.PostUnitOFWork.PostServices.BeginTransaction())
            {
                try
                {

                    Post Mapped_TextPost = UnitOFWork.Mapper.Map<Post>(request);

                    await UnitOFWork.PostUnitOFWork.PostServices.AddAsync(Mapped_TextPost);
                    await UnitOFWork.PostUnitOFWork.PostServices.SaveChangesAsync();

                    // 3. Get friend IDs (consider pagination for very large friend lists)
                    var UserIdOFFriends = await UnitOFWork.FriendUnitOFWork.FriendServices.GetFriendsIdOFUserByUserIdAsync(request.UserId_That_Want_To_AddPost);

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    var AllNotifications = UserIdOFFriends.Select(UserId =>
                    {
                        Notification.UserIdWhoReceivedTheNotification = UserId;
                        return Notification;
                    }).ToList();

                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.BulkInsertAsync(AllNotifications);


                    // 5. Bulk create post-notification relationships
                    var PostNotifications = AllNotifications.Select(Notification => new PostNotification
                    {
                        PostId = Mapped_TextPost.Id,
                        NotificationId = Notification.Id
                    }).ToList();

                    await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.BulkInsertAsync(PostNotifications);

                    // 6. Single transaction commit
                    await UnitOFWork.PostUnitOFWork.PostServices.CommitTransaction(Transaction);
                    // 2. Notify Online friends about the new post via hub
                    PostsOFUserQuery Mapped_PostOfUserQuery = UnitOFWork.Mapper.Map<PostsOFUserQuery>(Mapped_TextPost);
                    await UnitOFWork.RealTimeUnitOFWork.PostHubService.NotifyFriendsAboutPost(request.UserId_That_Want_To_AddPost, Mapped_PostOfUserQuery);

                    GetNotificationOFUser Mapped_GetNotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyFriendsAboutNewPost(request.UserId_That_Want_To_AddPost, Mapped_GetNotificationOFUser);
                    return Created<string>("Post created successfully");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                    return BadRequest<string>(ex.Message);
                }
            }
        }
        public async Task<Response<string>> Handle(AddImageOrVideoPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.PostUnitOFWork.PostServices.BeginTransaction())
            {
                (List<string> PathsOFImagesOrVideos, bool IsStoredSuccessfully) = (new List<string>(), false);
                try
                {
                    Post Mapped_ImageOrVideoPost = UnitOFWork.Mapper.Map<Post>(request);

                    await UnitOFWork.PostUnitOFWork.PostServices.AddAsync(Mapped_ImageOrVideoPost);
                    await UnitOFWork.PostUnitOFWork.PostServices.SaveChangesAsync();

                    (PathsOFImagesOrVideos, IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFiles(request.ImageOrVideos, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.AllowedExtension(),
                       UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(),
                       UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());


                    if (PathsOFImagesOrVideos.Contains(FilesConstants.ErrorExtensionFiles) || PathsOFImagesOrVideos.Contains(FilesConstants.ErrorSizeFiles) && !IsStoredSuccessfully)
                    {
                        await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Select(E => E).ToString()!);
                    }
                    else if (!IsStoredSuccessfully)
                    {
                        await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Select(E => E).ToString()!);
                    }

                    List<ImageOrVideoPath> AllPathOFImagesOrVideos = PathsOFImagesOrVideos.Select(Path => new ImageOrVideoPath()
                    {
                        PostId = Mapped_ImageOrVideoPost.Id,
                        Image_Or_VideoPath = Path
                    }).ToList();
                    await UnitOFWork.PostUnitOFWork.ImageOrVideoPathServices.BulkInsertAsync(AllPathOFImagesOrVideos);


                    var UserIdOFFriends = await UnitOFWork.FriendUnitOFWork.FriendServices.GetFriendsIdOFUserByUserIdAsync(request.UserId_That_Want_To_AddPost);

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    var AllNotifications = UserIdOFFriends.Select(UserId =>
                    {
                        Notification.UserIdWhoReceivedTheNotification = UserId;
                        return Notification;
                    }).ToList();

                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.BulkInsertAsync(AllNotifications);


                    // 5. Bulk create post-notification relationships
                    var PostNotifications = AllNotifications.Select(Notification => new PostNotification
                    {
                        PostId = Mapped_ImageOrVideoPost.Id,
                        NotificationId = Notification.Id
                    }).ToList();

                    await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.BulkInsertAsync(PostNotifications);

                    await UnitOFWork.PostUnitOFWork.PostServices.CommitTransaction(Transaction);

                    //Notify Online friends about the new post via hub
                    PostsOFUserQuery Mapped_PostsOFUserQuery = UnitOFWork.Mapper.Map<PostsOFUserQuery>(Mapped_ImageOrVideoPost);
                    await UnitOFWork.RealTimeUnitOFWork.PostHubService.NotifyFriendsAboutPost(request.UserId_That_Want_To_AddPost, Mapped_PostsOFUserQuery);
                    GetNotificationOFUser Mapped_GetNotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyFriendsAboutNewPost(request.UserId_That_Want_To_AddPost, Mapped_GetNotificationOFUser);

                    return Created<string>("Post Is Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                    await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFiles(PathsOFImagesOrVideos, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn());
                    Logger.Error(ex, "Error creating media post");
                    return BadRequest<string>("An error occurred while creating the post");
                }
            }
        }
        public async Task<Response<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.PostUnitOFWork.PostServices.BeginTransaction())
            {

                try
                {

                    Post post = await UnitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(request.Id);
                    post.IsDeleted = true;

                    await UnitOFWork.PostUnitOFWork.PostServices.SaveChangesAsync();
                    await Transaction.CommitAsync();

                    return OK("Post Is Deleted Successfully");



                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                    return BadRequest<string>(ex.Message);
                }


            }

        }

        public async Task<Response<string>> Handle(UpdatePostCommand request, CancellationToken cancellation)
        {
            using (var Transaction = await UnitOFWork.PostUnitOFWork.PostServices.BeginTransaction())
            {
                try
                {
                    var Post = await UnitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(request.PostId);
                    Post.IsUpdated = true;
                    Post.CreatedDate = DateTimeOffset.Now;
                    Post.Content = request.New_Content;

                    await UnitOFWork.PostUnitOFWork.PostServices.UpdateAsync(Post);
                    await UnitOFWork.PostUnitOFWork.PostServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Updated Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.PostUnitOFWork.PostServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message);
                    return BadRequest<string>(ex.Message);
                }
            }

        }

        public async Task<Response<string>> Handle(UpdateMediaPostCommand request, CancellationToken cancellation)
        {
            using (var Transaction = await UnitOFWork.PostUnitOFWork.ImageOrVideoPathServices.BeginTransaction())
            {
                try
                {

                    var Post = await UnitOFWork.PostUnitOFWork.PostServices.GetPostWithImageOrVideoPathsById(request.PostId);
                    Post.IsUpdated = true;
                    Post.CreatedDate = DateTimeOffset.Now;
                    Post.Caption = request.Caption;

                    (List<string> PathsOFImagesOrVideos, bool IsStoredSuccessfully) = (new List<string>(), false);
                    (PathsOFImagesOrVideos, IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFiles(request.Media, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.AllowedExtension(),
                      UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(),
                      UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());

                    if (PathsOFImagesOrVideos.Contains(FilesConstants.ErrorExtensionFiles) || PathsOFImagesOrVideos.Contains(FilesConstants.ErrorSizeFiles) && !IsStoredSuccessfully)
                    {
                        await UnitOFWork.PostUnitOFWork.ImageOrVideoPathServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Select(E => E).ToString()!);
                    }
                    else if (!IsStoredSuccessfully)
                    {
                        await UnitOFWork.PostUnitOFWork.ImageOrVideoPathServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Select(E => E).ToString()!);
                    }


                    foreach (var Item in Post.ImageOrVideo_Paths)
                    {
                        Item.IsDeleted = true;
                    }
                    await UnitOFWork.PostUnitOFWork.PostServices.UpdateAsync(Post);
                    List<ImageOrVideoPath> AllPathOFImagesOrVideos = PathsOFImagesOrVideos.Select(Path => new ImageOrVideoPath()
                    {
                        PostId = Post.Id,
                        Image_Or_VideoPath = Path
                    }).ToList();
                    await UnitOFWork.PostUnitOFWork.ImageOrVideoPathServices.BulkInsertAsync(AllPathOFImagesOrVideos);
                    await UnitOFWork.PostUnitOFWork.PostServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Updated Successfully");
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    Logger.Error(ex.Message);
                    return BadRequest<string>(ex.Message);
                }

            }
        }
    }


}
