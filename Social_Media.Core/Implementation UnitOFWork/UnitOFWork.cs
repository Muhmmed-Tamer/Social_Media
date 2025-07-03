using AutoMapper;
using Microsoft.Extensions.Configuration;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Data;
using Social_Media.Services.AbstractsServices.UserConnectionServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels.Authentication_Services;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class UnitOFWork : IUnitOFWork
    {
        public UnitOFWork(IConfiguration Configuration, IAuthenticationServices AuthenticationServices, IChatUnitOFWork ChatUnitOFWork, ICommentUnitOFWork CommentUnitOFWork, IConfigurationOFFilesUnitOFWork ConfigurationOfFilesUnitOFWork, IProtocolAndHostServices ProtocolAndHostServices, IExternalNotificationUnitOFWork ExternalNotificationUnitOFWork, IFriendUnitOFWork FriendUnitOFWork, IIdentityUnitOFWork IdentityUnitOFWork, IInteractionUnitOFWork InteractionUnitOFWork, IMapper Mapper, INotificationUnitOFWork NotificationUnitOFWork, IPostUnitOFWork PostUnitOFWork, IRealTimeUnitOFWork RealTimeUnitOFWork, IStoryUnitOFWork StoryUnitOFWork, IUserConnectionServices UserConnectionServices,ContextData contextData)
        {
            this.Configuration = Configuration;
            this.AuthenticationServices = AuthenticationServices;
            this.ChatUnitOFWork = ChatUnitOFWork;
            this.CommentUnitOFWork = CommentUnitOFWork;
            this.ConfigurationOfFilesUnitOFWork = ConfigurationOfFilesUnitOFWork;
            this.ProtocolAndHostServices = ProtocolAndHostServices;
            this.ExternalNotificationUnitOFWork = ExternalNotificationUnitOFWork;
            this.FriendUnitOFWork = FriendUnitOFWork;
            this.IdentityUnitOFWork = IdentityUnitOFWork;
            this.InteractionUnitOFWork = InteractionUnitOFWork;
            this.Mapper = Mapper;
            this.NotificationUnitOFWork = NotificationUnitOFWork;
            this.PostUnitOFWork = PostUnitOFWork;
            this.RealTimeUnitOFWork = RealTimeUnitOFWork;
            this.StoryUnitOFWork = StoryUnitOFWork;
            this.UserConnectionServices = UserConnectionServices;
            this.ContextData = contextData; 
        }

        public IConfiguration Configuration { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IChatUnitOFWork ChatUnitOFWork { get; }
        public ICommentUnitOFWork CommentUnitOFWork { get; }
        public IConfigurationOFFilesUnitOFWork ConfigurationOfFilesUnitOFWork { get; }
        public IProtocolAndHostServices ProtocolAndHostServices { get; }

        public IExternalNotificationUnitOFWork ExternalNotificationUnitOFWork { get; }
        public IFriendUnitOFWork FriendUnitOFWork { get; }
        public IIdentityUnitOFWork IdentityUnitOFWork { get; }
        public IInteractionUnitOFWork InteractionUnitOFWork { get; }
        public IMapper Mapper { get; }
        public INotificationUnitOFWork NotificationUnitOFWork { get; }
        public IPostUnitOFWork PostUnitOFWork { get; }
        public IRealTimeUnitOFWork RealTimeUnitOFWork { get; }
        public IStoryUnitOFWork StoryUnitOFWork { get; }
        public IUserConnectionServices UserConnectionServices { get; }

        public ContextData ContextData { get; }

    }
}
