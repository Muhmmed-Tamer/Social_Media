using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.NewMessageSentNotification_Mapping
{
    public partial class NewMessageNotificationProfile
    {
        public void MappingNewMessageNotification()
        {
            CreateMap<AddTextMessageCommand, Notification>()
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.UserIdWhoReceivedTheNotification, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.SendMessage))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "New Message"))
                .ReverseMap();
        }

        public void MappingNewAudioMessageNotification()
        {
            CreateMap<AddAudioMessageCommand, Notification>()
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.UserIdWhoReceivedTheNotification, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.SendMessage))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "New Audio Message"))
                .ReverseMap();
        }
        public void MappingNewMediaMessageNotification()
        {
            CreateMap<AddMediaMessageCommand, Notification>()
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.UserIdWhoReceivedTheNotification, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.SendMessage))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "New Media Message"))
                .ReverseMap();
        }
    }
}
