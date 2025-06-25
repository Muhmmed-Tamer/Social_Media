using AutoMapper;
using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Social_Media.Core.Abstracts_UnitOFWork;
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

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class UnitOFWork : IUnitOFWork
    {
        #region All Classes & Interfaces
        public IUserServices UserServices { get; }

        public RoleManager<IdentityRole> ManagerRole { get; }

        public IFileService FileServices { get; }

        public IMessageServices MessageServices { get; }

        public IFriendServices FriendServices { get; }

        public IInteractionWithStoryServices InteractionWithStoryServices { get; }

        public IInteractionWithPostServices InteractionWithPostServices { get; }

        public IInteractionWithCommentServices InteractionWithCommentServices { get; }

        public IPostNotificationServices PostNotificationServices { get; }

        public IMessageNotificationServices MessageNotificationServices { get; }

        public IFriendRequestServices FriendRequestServices { get; }

        public IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }

        public IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }

        public IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }

        public IConfiguration Configuration { get; }

        public IMapper Mapper { get; }

        public IImageOrVideoPathServices ImageOrVideoPathServices { get; }
        public IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices { get; }

        public IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices { get; }

        public IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices { get; }

        public IUserConnectionServices UserConnectionServices { get; }

        public PostHubServices PostHubService { get; }

        public IAuthenticationServices AuthenticationServices { get; }

        public ICommentServices CommentServices { get; }

        public IPostServices PostServices { get; }
        public IProtocolAndHostServices ProtocolAndHostServices { get; }

        public IEmailServices EmailServices { get; }

        public ISMSServices SMSServices { get; }

        public IImageOrVideoStoryPathService ImageOrVideoStoryPathService { get; }


        public IStoryService StoryService { get; }

        public IStoryService StoryServices { get; }

        public CommentHubServices CommentHubService { get; }

        public INotificationServices NotificationServices { get; }

        public ICommentNotificationServices CommentNotificationService { get; }

        public NotificationHubServices NotificationHubService { get; }

        public ISendFriendRequestNotificationServices SendFriendRequestNotificationService { get; }

        public IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService { get; }

        public InteractionWithPostHubServices InteractionWithPostHubService { get; }

        public InteractionWithCommentHubServices InteractionWithCommentHubService { get; }

        #endregion
        #region Constructor 
        public UnitOFWork(IUserServices UserServices, RoleManager<IdentityRole> ManagerRole, IFileService FileServices, IPostServices PostServices,
            IMessageServices MessageServices, IFriendServices FriendServices, IInteractionWithStoryServices InteractionWithStoryServices, IInteractionWithPostServices InteractionWithPostServices, IInteractionWithCommentServices InteractionWithCommentServices,
            IPostNotificationServices PostNotificationServices, IMessageNotificationServices MessageNotificationServices, IFriendRequestServices FriendRequestServices, IInteractionNotificationByStoryServices InteractionNotificationByStoryServices,
            IInteractionNotificationByPostServices InteractionNotificationByPostServices, IInteractionNotificationByCommentServices InteractionNotificationByCommentServices, IConfiguration Configuration, IMapper Mapper, IImageOrVideoPathServices ImageOrVideoPathServices, IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices, IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices,
            IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices, IUserConnectionServices UserConnectionServices, PostHubServices PostHubService,
            IAuthenticationServices AuthenticationServices, ICommentServices CommentServices, IProtocolAndHostServices ProtocolAndHostServices, IEmailServices EmailServices, ISMSServices SMSServices
            , IImageOrVideoStoryPathService imageOrVideoStoryPathService, IStoryService storyService, CommentHubServices CommentHubService, INotificationServices NotificationServices,
            NotificationHubServices NotificationHubService, ICommentNotificationServices CommentNotificationService, ISendFriendRequestNotificationServices SendFriendRequestNotificationService,
            IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService, InteractionWithPostHubServices InteractionWithPostHubService, InteractionWithCommentHubServices InteractionWithCommentHubService
            )
        {
            this.ProtocolAndHostServices = ProtocolAndHostServices;
            this.UserServices = UserServices;
            this.ManagerRole = ManagerRole;
            this.FileServices = FileServices;
            this.PostServices = PostServices;
            this.MessageServices = MessageServices;
            this.FriendServices = FriendServices;
            this.InteractionWithStoryServices = InteractionWithStoryServices;
            this.InteractionWithPostServices = InteractionWithPostServices;
            this.InteractionWithCommentServices = InteractionWithCommentServices;
            this.PostNotificationServices = PostNotificationServices;
            this.MessageNotificationServices = MessageNotificationServices;
            this.FriendRequestServices = FriendRequestServices;
            this.InteractionNotificationByStoryServices = InteractionNotificationByStoryServices;
            this.InteractionNotificationByPostServices = InteractionNotificationByPostServices;
            this.InteractionNotificationByCommentServices = InteractionNotificationByCommentServices;
            this.ImageOrVideoPathServices = ImageOrVideoPathServices;
            this.Configuration = Configuration;
            this.ConfigurationOFPostImageServices = ConfigurationOFPostImageServices;
            this.ConfigurationOFPostVideoServices = ConfigurationOFPostVideoServices;
            this.ConfigurationOFUserImageServices = ConfigurationOFUserImageServices;
            this.UserConnectionServices = UserConnectionServices;
            this.PostHubService = PostHubService;
            this.AuthenticationServices = AuthenticationServices;
            this.CommentServices = CommentServices;
            this.SMSServices = SMSServices;
            this.EmailServices = EmailServices;
            this.Mapper = Mapper;
            this.ImageOrVideoStoryPathService = imageOrVideoStoryPathService;
            this.CommentHubService = CommentHubService;
            this.StoryService = storyService;
            this.NotificationServices = NotificationServices;
            this.NotificationHubService = NotificationHubService;
            this.CommentNotificationService = CommentNotificationService;
            this.SendFriendRequestNotificationService = SendFriendRequestNotificationService;
            this.ConfirmFriendRequestNotificationService = ConfirmFriendRequestNotificationService;
            this.InteractionWithPostHubService = InteractionWithPostHubService;
            this.InteractionWithCommentHubService = InteractionWithCommentHubService;
        }
        #endregion
    }
}
