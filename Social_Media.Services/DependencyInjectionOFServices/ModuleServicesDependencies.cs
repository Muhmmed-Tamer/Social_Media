using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
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
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.ChatServices;
using Social_Media.Services.AbstractsServices.CommentServices;
using Social_Media.Services.AbstractsServices.FriendsServices;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.INotificationsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServices.StoryServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServices.ChatServices;
using Social_Media.Services.ImplementationServices.CommentServices;
using Social_Media.Services.ImplementationServices.FriendServices;
using Social_Media.Services.ImplementationServices.IdentityServices.IdentityRole;
using Social_Media.Services.ImplementationServices.IdentityServices.IdentityUser;
using Social_Media.Services.ImplementationServices.InteractionsServices;
using Social_Media.Services.ImplementationServices.NotificationsServices;
using Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification;
using Social_Media.Services.ImplementationServices.PostServices;
using Social_Media.Services.ImplementationServices.StoryServices;
using Social_Media.Services.ImplementationServicesOFSpecialModels;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Image_Configurations;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Video_Configurations;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.ProtocolAndHosts_Services;
namespace Social_Media.Services.DependencyInjectionOFServices
{
    public static class ModuleServicesDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection Services)
        {
            #region Interaction Notifications
            Services.AddScoped<IServices<InteractionNotificationByStory>, Services<InteractionNotificationByStory>>();
            Services.AddScoped<IServices<InteractionNotificationByPost>, Services<InteractionNotificationByPost>>();
            Services.AddScoped<IServices<InteractionNotificationByComment>, Services<InteractionNotificationByComment>>();
            Services.AddScoped<IServices<CommentNotification>, Services<CommentNotification>>();
            Services.AddScoped<IServices<Notification>, Services<Notification>>();
            Services.AddScoped<ICommentNotificationServices, CommentNotificationServices>();
            #endregion
            #region External Notifications
            Services.AddScoped<IEmailServices, EmailServices>();
            Services.AddScoped<ISMSServices, SMSServices>();
            #endregion
            #region UserConnection
            Services.AddScoped<IServices<UserConnection>, Services<UserConnection>>();
            #endregion        
            #region Authentication
            Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            #endregion
            #region Domain OF App
            Services.AddScoped<IProtocolAndHostServices, ProtocolAndHostServices>();
            #endregion

            #region Notifications
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            Services.AddScoped<IMessageNotificationServices, MessageNotificationServices>();
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            Services.AddScoped<IServices<MessageNotification>, Services<MessageNotification>>();
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            Services.AddScoped<IServices<SendFriendRequestNotification>, Services<SendFriendRequestNotification>>();
            Services.AddScoped<IServices<ConfirmFriendRequestNotification>, Services<ConfirmFriendRequestNotification>>();
            Services.AddScoped<INotificationServices, NotificationServices>();
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            Services.AddScoped<ICommentNotificationServices, CommentNotificationServices>();
            Services.AddScoped<IMessageNotificationServices, MessageNotificationServices>();
            Services.AddScoped<IInteractionNotificationByPostServices, InteractionNotificationByPostServices>();
            Services.AddScoped<IInteractionNotificationByCommentServices, InteractionNotificationByCommentServices>();
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            #endregion
            #region Interactions
            Services.AddScoped<IInteractionWithStoryServices, InteractionWithStoryServices>();
            Services.AddScoped<IInteractionWithPostServices, InteractionWithPostServices>();
            Services.AddScoped<IInteractionWithCommentServices, InteractionWithCommentServices>();
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            Services.AddScoped<IInteractionNotificationByPostServices, InteractionNotificationByPostServices>();
            Services.AddScoped<IInteractionNotificationByCommentServices, InteractionNotificationByCommentServices>();
            Services.AddScoped<IConfirmFriendRequestNotificationServices, ConfirmFriendRequestNotificationServices>();
            Services.AddScoped<ISendFriendRequestNotificationServices, SendFriendRequestNotificationServices>();
            Services.AddScoped<IServices<InteractionWithComment>, Services<InteractionWithComment>>();
            Services.AddScoped<IServices<InteractionWithStory>, Services<InteractionWithStory>>();
            Services.AddScoped<IServices<InteractionWithPost>, Services<InteractionWithPost>>();
            Services.AddScoped<ICommentNotificationServices, CommentNotificationServices>();
            #endregion
            #region Identity
            Services.AddScoped<IServices<UserRefreshToken>, Services<UserRefreshToken>>();
            Services.AddScoped<IUserRefreshTokenServices, UserRefreshTokenServices>();
            Services.AddScoped<IUserServices, UserServices>();
            Services.AddScoped<IRoleServices, RoleServices>();
            #endregion
            #region Comment
            Services.AddScoped<IServices<ReplyOFComment>, Services<ReplyOFComment>>();
            Services.AddScoped<IReplyOFCommentServices, ReplyOFCommentServices>();
            Services.AddScoped<IServices<Comment>, Services<Comment>>();
            Services.AddScoped<ICommentServices, CommentServices>();
            #endregion
            #region Friend
            Services.AddScoped<IServices<Friend>, Services<Friend>>();
            Services.AddScoped<IFriendServices, FriendServices>();
            #endregion
            #region Posts 
            Services.AddScoped<IImageOrVideoPathServices, ImageOrVideoPathServices>();
            Services.AddScoped<IPostServices, PostServices>();
            Services.AddScoped<IServices<Post>, Services<Post>>();
            Services.AddScoped<IServices<ImageOrVideoPath>, Services<ImageOrVideoPath>>();
            #endregion
            #region Files
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostImageServices>, ConfigurationOFPostImageServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostVideoServices>, ConfigurationOFPostVideoServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFUserImageServices>, ConfigurationOFUserImageServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFChatAudioServices>, ConfigurationOFChatAudioServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFChatVideoServices>, ConfigurationOFChatVideoServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFChatImageServices>, ConfigurationOFChatImageServices>();
            Services.AddScoped<IFileService, FileService>();
            #endregion
            #region Story
            Services.AddScoped<IServices<Story>, Services<Story>>();
            Services.AddScoped<IServices<ImageOrVideoStoryPath>, Services<ImageOrVideoStoryPath>>();
            Services.AddScoped<IImageOrVideoStoryPathService, ImageOrVideoStoryPathService>();
            Services.AddScoped<IStoryService, StoryService>();
            #endregion
            #region Chat
            Services.AddScoped<IServices<MessageMediaPath>, Services<MessageMediaPath>>();
            Services.AddScoped<IMessageMediaPathServices, MessageMediaPathServices>();
            Services.AddScoped<IServices<Message>, Services<Message>>();
            Services.AddScoped<IMessageServices, MessageServices>();
            Services.AddScoped<IMessageServices, MessageServices>();
            #endregion
        }
    }
}
