using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Identity;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.ChatRepository;
using Social_Media.InfraStructure.AbstractsRepositories.CommentRepository;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.InfraStructure.AbstractsRepositories.IdentityUser_Repository;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;
using Social_Media.InfraStructure.ImplementationRepositories;
using Social_Media.InfraStructure.ImplementationRepositories.ChatRepository;
using Social_Media.InfraStructure.ImplementationRepositories.CommentRepository;
using Social_Media.InfraStructure.ImplementationRepositories.IdentityUser_Repository;
using Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.AddFriendRequest_Notification;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.MessagesNotificationRepository;
using Social_Media.InfraStructure.ImplementationRepositories.PostsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.UserConnection;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.DependencyInjectionOFInfraStructure
{
    public static class ModuleInfraStructureDependencies
    {
        public static void AddInfraStructureDependencies(this IServiceCollection Services)
        {
            #region Interactions Notifications
            Services.AddScoped<IRepository<InteractionNotificationByPost>, Repository<InteractionNotificationByPost>>();
            Services.AddScoped<IRepository<InteractionNotificationByComment>, Repository<InteractionNotificationByComment>>();
            Services.AddScoped<IRepository<InteractionNotificationByStory>, Repository<InteractionNotificationByStory>>();
            Services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            #endregion
            #region UserConnection
            Services.AddScoped<IRepository<UserConnection>, Repository<UserConnection>>();
            Services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            #endregion
            #region Notifications
            Services.AddScoped<IRepository<ConfirmFriendRequestNotification>, Repository<ConfirmFriendRequestNotification>>();
            Services.AddScoped<IConfirmFriendRequestNotificationRepository, ConfirmFriendRequestNotificationRepository>();
            Services.AddScoped<IRepository<SendFriendRequestNotification>, Repository<SendFriendRequestNotification>>();
            Services.AddScoped<ISendFriendRequestNotificationRepository, SendFriendRequestNotificationRepository>();
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            Services.AddScoped<IRepository<CommentNotification>, CommentNotificationRepository>();
            Services.AddScoped<ICommentNotificationRepository, CommentNotificationRepository>();
            Services.AddScoped<IRepository<PostNotification>, Repository<PostNotification>>();
            Services.AddScoped<IPostNotificationRepository, PostNotificationRepository>();
            Services.AddScoped<ICommentNotificationRepository, CommentNotificationRepository>();
            Services.AddScoped<IInteractionWithCommentRepository, InteractionWithCommentRepository>();
            Services.AddScoped<IInteractionNotificationByCommentRepository, InteractionNotificationByCommentRepository>();
            Services.AddScoped<IInteractionNotificationByStoryRepository, InteractionNotificationByStoryRepository>();
            Services.AddScoped<IInteractionNotificationByPostRepository, InteractionNotificationByPostRepository>();
            Services.AddScoped<IMessageNotificationRepository, MessageNotificationRepository>();
            Services.AddScoped<INotificationRepository, NotificationRepository>();
            #endregion
            #region Interactions
            Services.AddScoped<IRepository<InteractionWithComment>, Repository<InteractionWithComment>>();
            Services.AddScoped<IRepository<InteractionWithStory>, Repository<InteractionWithStory>>();
            Services.AddScoped<IInteractionWithCommentRepository, InteractionWithCommentRepository>();
            Services.AddScoped<IRepository<InteractionWithPost>, Repository<InteractionWithPost>>();
            Services.AddScoped<IInteractionWithPostRepository, InteractionWithPostRepository>();
            #endregion
            #region Identity
            Services.AddScoped<IRepository<UserRefreshToken>, Repository<UserRefreshToken>>();
            Services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            #endregion
            #region Comment
            Services.AddScoped<IRepository<ReplyOFComment>, Repository<ReplyOFComment>>();
            Services.AddScoped<IReplyOFCommentRepository, ReplyOFCommentRepository>();
            Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            Services.AddScoped<ICommentRepository, CommentRepository>();
            #endregion
            #region Friends
            Services.AddScoped<IRepository<FriendRequest>, Repository<FriendRequest>>();
            Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            Services.AddScoped<IRepository<Friend>, Repository<Friend>>();
            Services.AddScoped<IFriendRepository, FriendRepository>();
            #endregion
            #region Posts
            Services.AddScoped<IRepository<ImageOrVideoPath>, Repository<ImageOrVideoPath>>();
            Services.AddScoped<IImageOrVideoPathRepository, ImageOrVideoPathRepository>();
            Services.AddScoped<IRepository<Post>, Repository<Post>>();
            Services.AddScoped<IPostRepository, PostRepository>();
            Services.AddScoped<ImageOrVideoPathRepository>();
            #endregion
            #region Story
            Services.AddScoped<IRepository<Story>, Repository<Story>>();
            Services.AddScoped<IRepository<ImageOrVideoStoryPath>, Repository<ImageOrVideoStoryPath>>();
            #endregion
            #region Chat
            Services.AddScoped<IMessageMediaPathRepository, MessageMediaPathRepository>();
            Services.AddScoped<IRepository<MessageMediaPath>, Repository<MessageMediaPath>>();
            Services.AddScoped<IRepository<Message>, Repository<Message>>();
            Services.AddScoped<IMessageRepository, MessageRepository>();
            #endregion
        }
    }
}
