using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.Notification_Mapping
{
    public partial class NotificationProfile
    {
        public void AddCommentToPostNotificationMapping()
        {
            CreateMap<AddCommentToPostCommand, Notification>()
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(__ => Data.Enums.NotificationType.AddComment))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(__ => "New Comment Added"))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserId))
                .ReverseMap();
        }
    }
}
