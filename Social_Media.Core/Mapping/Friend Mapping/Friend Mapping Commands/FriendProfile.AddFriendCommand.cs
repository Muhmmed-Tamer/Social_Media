using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Data.Models.Friends;

namespace Social_Media.Core.Mapping.Friend_Mapping
{
    public partial class FriendProfile
    {
        public void MappingAddFriendCommandFromFriendRequestNotification()
        {
            CreateMap<ConfirmFriendRequestCommand, Friend>()
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserIdThatSentFriendRequest))
                .ForMember(d => d.FriendUserId, Opt => Opt.MapFrom(S => S.FriendUserIdThatConfirmFriendRequest))
                .ReverseMap();
        }
    }
}
