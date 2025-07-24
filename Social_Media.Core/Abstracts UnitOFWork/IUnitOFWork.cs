using AutoMapper;
using Microsoft.Extensions.Configuration;
using Social_Media.Data;
using Social_Media.Services.AbstractsServices.UserConnectionServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IUnitOFWork
    {
        #region All Classes & Interfaces
        IConfiguration Configuration { get; }
        IAuthenticationServices AuthenticationServices { get; }
        IChatUnitOFWork ChatUnitOFWork { get; }
        ICommentUnitOFWork CommentUnitOFWork { get; }
        IConfigurationOFFilesUnitOFWork ConfigurationOfFilesUnitOFWork { get; }
        IProtocolAndHostServices ProtocolAndHostServices { get; }
        IExternalNotificationUnitOFWork ExternalNotificationUnitOFWork { get; }
        IFriendUnitOFWork FriendUnitOFWork { get; }
        IIdentityUnitOFWork IdentityUnitOFWork { get; }
        IInteractionUnitOFWork InteractionUnitOFWork { get; }
        IMapper Mapper { get; }

        INotificationUnitOFWork NotificationUnitOFWork { get; }
        IPostUnitOFWork PostUnitOFWork { get; }
        IRealTimeUnitOFWork RealTimeUnitOFWork { get; }
        IStoryUnitOFWork StoryUnitOFWork { get; }
        IUserConnectionServices UserConnectionServices { get; }
        ContextData ContextData { get; }    

        #endregion
    }
}
