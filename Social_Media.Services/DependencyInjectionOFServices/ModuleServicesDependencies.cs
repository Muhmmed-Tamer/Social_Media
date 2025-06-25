using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using Social_Media.Data.Models.Story;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.INotificationsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServices.IdentityServices.IdentityUser;
using Social_Media.Services.ImplementationServices.NotificationsServices;
using Social_Media.Services.ImplementationServices.NotificationsServices.FriendRequestNotification;
using Social_Media.Services.ImplementationServicesOFSpecialModels;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.ProtocolAndHosts_Services;
namespace Social_Media.Services.DependencyInjectionOFServices
{
    public static class ModuleServicesDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<IServices<Comment>, Services<Comment>>();
            Services.AddScoped<IServices<Friend>, Services<Friend>>();
            Services.AddScoped<IServices<Message>, Services<Message>>();
            Services.AddScoped<IServices<UserConnection>, Services<UserConnection>>();
            Services.AddScoped<IServices<Comment>, Services<Comment>>();
            Services.AddScoped<ICommentServices, CommentServices>();
            Services.AddScoped<IProtocolAndHostServices, ProtocolAndHostServices>();
            Services.AddScoped<IUserServices, UserServices>();
            Services.AddScoped<INotificationServices, NotificationServices>();
            Services.AddScoped<ICommentNotificationServices, CommentNotificationServices>();
            Services.AddScoped<ISendFriendRequestNotificationServices, SendFriendRequestNotificationServices>();
            Services.AddScoped<IConfirmFriendRequestNotificationServices, ConfirmFriendRequestNotificationServices>();
            #region Notifications
            Services.AddScoped<IEmailServices, EmailServices>();
            Services.AddScoped<ISMSServices, SMSServices>();
            #endregion

            #region Authentication
            Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            #endregion

            #region Posts 
            Services.AddScoped<IServices<Post>, Services<Post>>();
            Services.AddScoped<IServices<ImageOrVideoPath>, Services<ImageOrVideoPath>>();
            #endregion
            #region Story
            Services.AddScoped<IServices<Story>, Services<Story>>();
            Services.AddScoped<IServices<ImageOrVideoStoryPath>, Services<ImageOrVideoStoryPath>>();
            #endregion

            #region Interactions
            Services.AddScoped<IServices<InteractionWithComment>, Services<InteractionWithComment>>();
            Services.AddScoped<IServices<InteractionWithPost>, Services<InteractionWithPost>>();
            Services.AddScoped<IServices<InteractionWithStory>, Services<InteractionWithStory>>();
            #endregion

            #region Notifications
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            Services.AddScoped<IServices<MessageNotification>, Services<MessageNotification>>();
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            Services.AddScoped<IServices<SendFriendRequestNotification>, Services<SendFriendRequestNotification>>();
            Services.AddScoped<IServices<ConfirmFriendRequestNotification>, Services<ConfirmFriendRequestNotification>>();
            #endregion

            #region Interaction Notifications
            Services.AddScoped<IServices<InteractionNotificationByStory>, Services<InteractionNotificationByStory>>();
            Services.AddScoped<IServices<InteractionNotificationByPost>, Services<InteractionNotificationByPost>>();
            Services.AddScoped<IServices<InteractionNotificationByComment>, Services<InteractionNotificationByComment>>();
            Services.AddScoped<IServices<CommentNotification>, Services<CommentNotification>>();
            Services.AddScoped<IServices<Notification>, Services<Notification>>();
            Services.AddScoped<ICommentNotificationServices, CommentNotificationServices>();
            #endregion

            #region Files
            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostImageServices>, ConfigurationOFPostImageServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostVideoServices>, ConfigurationOFPostVideoServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFUserImageServices>, ConfigurationOFUserImageServices>();
            #endregion

        }
    }
}
