using AutoMapper;
using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Data.Identity;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServices.StoryServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.ImplementationServices.StoryServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class UnitOFWork : IUnitOFWork
    {
        #region All Classes & Interfaces
        public UserManager<ApplicationUser> ManagerUser { get; }

        public RoleManager<IdentityRole> ManagerRole { get; }

        public IFileService FileServices { get; }

        public IMessageServices MessageServices { get; }

        public IFriendServices FriendServices { get; }

        public IInteractionWithStoryServices InteractionWithStoryServices { get; }

        public IInteractionWithPostServices InteractionWithPostServices { get; }

        public IInteractionWithCommentServices InteractionWithCommentServices { get; }

        public IPostNotificationServices PostNotificationServices { get; }

        public IMessageNotificationServices MessageNotificationServices { get; }

        public IFriendRequestNotificationServices FriendRequestNotificationServices { get; }

        public IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }

        public IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }

        public IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }

        public IConfiguration Configuration { get; }

        public IMapper Mapper { get; }

        public ITextPostServices TextPostServices { get; }

        public IImageOrVideoPostServices ImageOrVideoPostServices { get; }

        public IImageOrVideoPathServices ImageOrVideoPathServices { get; }// It's For Post

        public IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices { get; }

        public IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices { get; }

        public IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices { get; }

      

        
        public IImageOrVideoStoryPathService ImageOrVideoStoryPathService {  get; }

      

        public IStoryService StoryService { get; }
        #endregion
        #region Constructor 
        public UnitOFWork(UserManager<ApplicationUser> ManagerUser, RoleManager<IdentityRole> ManagerRole, IFileService FileServices, ITextPostServices TextPostServices,
            IMessageServices MessageServices, IFriendServices FriendServices, IInteractionWithStoryServices InteractionWithStoryServices, IInteractionWithPostServices InteractionWithPostServices, IInteractionWithCommentServices InteractionWithCommentServices,
            IPostNotificationServices PostNotificationServices, IMessageNotificationServices MessageNotificationServices, IFriendRequestNotificationServices FriendRequestNotificationServices, IInteractionNotificationByStoryServices InteractionNotificationByStoryServices,
            IInteractionNotificationByPostServices InteractionNotificationByPostServices, IInteractionNotificationByCommentServices InteractionNotificationByCommentServices, IConfiguration Configuration, IMapper Mapper, IImageOrVideoPostServices ImageOrVideoPostServices,
            IImageOrVideoPathServices ImageOrVideoPathServices, IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices, IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices,
            IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices,IImageOrVideoStoryPathService imageOrVideoStoryPathService,IStoryService storyService
            )
        {
            this.ManagerUser = ManagerUser;
            this.ManagerRole = ManagerRole;
            this.FileServices = FileServices;
            this.TextPostServices = TextPostServices;
            this.ImageOrVideoPostServices = ImageOrVideoPostServices;
            this.MessageServices = MessageServices;
            this.FriendServices = FriendServices;
            this.InteractionWithStoryServices = InteractionWithStoryServices;
            this.InteractionWithPostServices = InteractionWithPostServices;
            this.InteractionWithCommentServices = InteractionWithCommentServices;
            this.PostNotificationServices = PostNotificationServices;
            this.MessageNotificationServices = MessageNotificationServices;
            this.FriendRequestNotificationServices = FriendRequestNotificationServices;
            this.InteractionNotificationByStoryServices = InteractionNotificationByStoryServices;
            this.InteractionNotificationByPostServices = InteractionNotificationByPostServices;
            this.InteractionNotificationByCommentServices = InteractionNotificationByCommentServices;
            this.ImageOrVideoPathServices = ImageOrVideoPathServices;
            this.Configuration = Configuration;
            this.ConfigurationOFPostImageServices = ConfigurationOFPostImageServices;
            this.ConfigurationOFPostVideoServices = ConfigurationOFPostVideoServices;
            this.ConfigurationOFUserImageServices = ConfigurationOFUserImageServices;
            this.Mapper = Mapper;
            this.ImageOrVideoStoryPathService = imageOrVideoStoryPathService;
            this.StoryService = storyService;
           
        }
        #endregion
    }
}
