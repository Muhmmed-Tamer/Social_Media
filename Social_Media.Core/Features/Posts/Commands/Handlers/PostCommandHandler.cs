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
        IRequestHandler<AddImageOrVideoPostCommand, Response<string>>
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
            using (var Transaction = await UnitOFWork.PostServices.BeginTransaction())
            {
                try
                {

                    Post Mapped_TextPost = UnitOFWork.Mapper.Map<Post>(request);
                    Mapped_TextPost.PostType = Data.Enums.PostType.Text;

                    await UnitOFWork.PostServices.AddAsync(Mapped_TextPost);
                    await UnitOFWork.PostServices.SaveChangesAsync();

                    // 3. Get friend IDs (consider pagination for very large friend lists)
                    var UserIdOFFriends = await UnitOFWork.FriendServices.GetFriendsIdOFUserByUserIdAsync(request.UserId_That_Want_To_AddPost);

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    var AllNotifications = UserIdOFFriends.Select(UserId =>
                    {
                        Notification.UserIdWhoReceivedTheNotification = UserId;
                        return Notification;
                    }).ToList();

                    await UnitOFWork.NotificationServices.BulkInsertAsync(AllNotifications);


                    // 5. Bulk create post-notification relationships
                    var PostNotifications = AllNotifications.Select(Notification => new PostNotification
                    {
                        PostId = Mapped_TextPost.Id,
                        NotificationId = Notification.Id
                    }).ToList();

                    await UnitOFWork.PostNotificationServices.BulkInsertAsync(PostNotifications);

                    // 6. Single transaction commit
                    await UnitOFWork.PostServices.CommitTransaction(Transaction);
                    // 2. Notify Online friends about the new post via hub
                    PostsOFUserQuery Mapped_PostOfUserQuery = UnitOFWork.Mapper.Map<PostsOFUserQuery>(Mapped_TextPost);
                    await UnitOFWork.PostHubService.NotifyFriendsAboutPost(request.UserId_That_Want_To_AddPost, Mapped_PostOfUserQuery);

                    GetNotificationOFUser Mapped_GetNotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.NotificationHubService.NotifyFriendsAboutNewPost(request.UserId_That_Want_To_AddPost, Mapped_GetNotificationOFUser);
                    return Created<string>("Post created successfully");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    await UnitOFWork.PostServices.RollbackTransaction(Transaction);
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(AddImageOrVideoPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.PostServices.BeginTransaction())
            {
                (List<string>, bool) PathsOFImagesOrVideos = default;
                try
                {
                    Post Mapped_ImageOrVideoPost = UnitOFWork.Mapper.Map<Post>(request);
                    Mapped_ImageOrVideoPost.PostType = Data.Enums.PostType.ImageOrVideo;

                    await UnitOFWork.PostServices.AddAsync(Mapped_ImageOrVideoPost);
                    await UnitOFWork.PostServices.SaveChangesAsync();


                    PathsOFImagesOrVideos = await UnitOFWork.FileServices.GeneratePathOFFiles(request.ImageOrVideos, UnitOFWork.ConfigurationOFPostImageServices.MaxSize(), UnitOFWork.ConfigurationOFPostImageServices.AllowedExtension(),
                       UnitOFWork.ConfigurationOFPostImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOFPostVideoServices.MaxSize(), UnitOFWork.ConfigurationOFPostVideoServices.DirectoryThatStoreFileIn(),
                       UnitOFWork.ConfigurationOFPostVideoServices.AllowedExtension());

                    if (PathsOFImagesOrVideos.Item1.Contains(FilesConstants.ErrorExtensionFiles) || PathsOFImagesOrVideos.Item1.Contains(FilesConstants.ErrorSizeFiles) && PathsOFImagesOrVideos.Item2 == false)
                    {
                        await UnitOFWork.PostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Item1.Select(E => E).ToString()!);
                    }
                    else if (PathsOFImagesOrVideos.Item2 == false)
                    {
                        await UnitOFWork.PostServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(PathsOFImagesOrVideos.Item1.Select(E => E).ToString()!);
                    }

                    List<ImageOrVideoPath> AllPathOFImagesOrVideos = PathsOFImagesOrVideos.Item1.Select(Path => new ImageOrVideoPath()
                    {
                        PostId = Mapped_ImageOrVideoPost.Id,
                        Image_Or_VideoPath = Path
                    }).ToList();
                    await UnitOFWork.ImageOrVideoPathServices.BulkInsertAsync(AllPathOFImagesOrVideos);


                    var UserIdOFFriends = await UnitOFWork.FriendServices.GetFriendsIdOFUserByUserIdAsync(request.UserId_That_Want_To_AddPost);

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    var AllNotifications = UserIdOFFriends.Select(UserId =>
                    {
                        Notification.UserIdWhoReceivedTheNotification = UserId;
                        return Notification;
                    }).ToList();

                    await UnitOFWork.NotificationServices.BulkInsertAsync(AllNotifications);


                    // 5. Bulk create post-notification relationships
                    var PostNotifications = AllNotifications.Select(Notification => new PostNotification
                    {
                        PostId = Mapped_ImageOrVideoPost.Id,
                        NotificationId = Notification.Id
                    }).ToList();

                    await UnitOFWork.PostNotificationServices.BulkInsertAsync(PostNotifications);

                    await UnitOFWork.PostServices.CommitTransaction(Transaction);

                    //Notify Online friends about the new post via hub
                    PostsOFUserQuery Mapped_PostsOFUserQuery = UnitOFWork.Mapper.Map<PostsOFUserQuery>(Mapped_ImageOrVideoPost);
                    await UnitOFWork.PostHubService.NotifyFriendsAboutPost(request.UserId_That_Want_To_AddPost, Mapped_PostsOFUserQuery);
                    GetNotificationOFUser Mapped_GetNotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.NotificationHubService.NotifyFriendsAboutNewPost(request.UserId_That_Want_To_AddPost, Mapped_GetNotificationOFUser);

                    return Created<string>("Post Is Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.PostServices.RollbackTransaction(Transaction);
                    await UnitOFWork.FileServices.DeleteFiles(PathsOFImagesOrVideos.Item1);
                    Logger.Error(ex, "Error creating media post");
                    return BadRequest<string>("An error occurred while creating the post");
                }
            }
        }
    }
}
