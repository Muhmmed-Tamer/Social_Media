using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.ConfirmFriendRequest_Notification_Mapping
{
    public partial class ConfirmFriendRequestProfile
    {
        public void MappingConfirmFriendRequestCommand()
        {
            CreateMap<ConfirmFriendRequestCommand, Notification>()
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.FriendUserIdThatConfirmFriendRequest))
                .ForMember(d => d.UserIdWhoReceivedTheNotification, Opt => Opt.MapFrom(S => S.UserIdThatSentFriendRequest))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.ConfirmFriendRequest))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "Friend Request Is Accepted"))
                .ReverseMap();
        }
    }
}
