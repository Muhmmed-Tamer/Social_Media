using AutoMapper;
using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Social_Media.Data.Identity;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IUnitOFWork
    {
        #region All Classes & Interfaces
        UserManager<ApplicationUser> ManagerUser { get; }
        RoleManager<IdentityRole> ManagerRole { get; }
        IFileService FileServices { get; }
        ITextPostServices TextPostServices { get; }
        IImageOrVideoPostServices ImageOrVideoPostServices { get; }
        IMessageServices MessageServices { get; }
        IFriendServices FriendServices { get; }
        IInteractionWithStoryServices InteractionWithStoryServices { get; }
        IInteractionWithPostServices InteractionWithPostServices { get; }
        IInteractionWithCommentServices InteractionWithCommentServices { get; }
        IPostNotificationServices PostNotificationServices { get; }
        IMessageNotificationServices MessageNotificationServices { get; }
        IFriendRequestNotificationServices FriendRequestNotificationServices { get; }
        IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }
        IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }
        IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }
        IImageOrVideoPathServices ImageOrVideoPathServices { get; }
        IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices { get; }
        IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices { get; }
        IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices { get; }
        IConfiguration Configuration { get; }
        IMapper Mapper { get; }
        #endregion
    }
}
