using AutoMapper;
using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.RealTimeServices.ImplementationHubServices.CommentsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithCommentHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithPostHub;
using Social_Media.RealTimeServices.ImplementationHubServices.NotificationsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.PostsHub;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.FriendsServices;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.INotificationsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServices.StoryServices;
using Social_Media.Services.AbstractsServices.UserConnectionServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.Email_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Notification_Services.SMS_Notification_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IUnitOFWork
    {
        #region All Classes & Interfaces
        IUserServices UserServices { get; }
        RoleManager<IdentityRole> ManagerRole { get; }
        IFileService FileServices { get; }
        IPostServices PostServices { get; }
        IStoryService StoryServices { get; }
        IImageOrVideoStoryPathService ImageOrVideoStoryPathService { get; }

        IMessageServices MessageServices { get; }
        IFriendServices FriendServices { get; }
        ICommentServices CommentServices { get; }
        IInteractionWithStoryServices InteractionWithStoryServices { get; }
        IInteractionWithPostServices InteractionWithPostServices { get; }
        IInteractionWithCommentServices InteractionWithCommentServices { get; }
        IPostNotificationServices PostNotificationServices { get; }
        IMessageNotificationServices MessageNotificationServices { get; }
        IFriendRequestServices FriendRequestServices { get; }
        IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }
        IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }
        IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }
        IImageOrVideoPathServices ImageOrVideoPathServices { get; }  // It's For Post
        IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices { get; }
        IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices { get; }
        IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices { get; }
        PostHubServices PostHubService { get; }
        CommentHubServices CommentHubService { get; }
        NotificationHubServices NotificationHubService { get; }
        InteractionWithPostHubServices InteractionWithPostHubService { get; }
        InteractionWithCommentHubServices InteractionWithCommentHubService { get; }
        IAuthenticationServices AuthenticationServices { get; }
        IUserConnectionServices UserConnectionServices { get; }
        IProtocolAndHostServices ProtocolAndHostServices { get; }
        IEmailServices EmailServices { get; }
        ISMSServices SMSServices { get; }
        IConfiguration Configuration { get; }
        INotificationServices NotificationServices { get; }
        ICommentNotificationServices CommentNotificationService { get; }
        ISendFriendRequestNotificationServices SendFriendRequestNotificationService { get; }
        IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService { get; }
        IMapper Mapper { get; }
        #endregion
    }
}
