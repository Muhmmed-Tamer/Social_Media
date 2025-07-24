using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.AddNewFriendRequestNotification_Mapping
{
    public partial class FriendRequestNotificationProfile
    {
        public void MappingAddNewFriendRequestNotification()
        {
            CreateMap<AddFriendRequestCommand, Notification>()
                .ForMember(d => d.UserIdWhoReceivedTheNotification, Opt => Opt.MapFrom(S => S.UserId_ThatReceiveFriendRequest))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserId_ThatSentFriendRequest))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(_ => "New FriendRequest"))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(_ => NotificationType.SendNewFriendRequest))
                .ReverseMap();
        }
    }
}
