using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.InteractionNotificationWithCommentMapping
{
    public partial class InteractionNotificationWithCommentProfile
    {
        public void MappingAddInteractionWithCommentCommand()
        {
            CreateMap<AddInteractionWithCommentCommand, Notification>()
               .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.InteractionWithComment))
               .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "Interact With Comment"))
               .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserId))
               .ReverseMap();
        }

    }
}
