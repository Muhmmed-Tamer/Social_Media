using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.Core.Mapping.InteractionNotificationWithPost_Mapping
{
    public partial class InteractionNotificationWithPostProfile
    {
        public void MappingAddInteractionNotificationWithPost1()
        {
            CreateMap<AddInteractionWithPostCommand, Notification>()
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(__ => NotificationType.InteractionWithPost))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(__ => "Interact With Post"))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserIdThatInteractWithPost))
                .ReverseMap();
        }

        public void MappingAddInteractionNotificationWithPost2()
        {
            CreateMap<AddInteractionWithPostCommand, InteractionNotificationByPost>()
                .ForMember(d => d.PostId, Opt => Opt.MapFrom(S => S.PostIdThatInteractWith))
                .ReverseMap();
        }
    }
}
