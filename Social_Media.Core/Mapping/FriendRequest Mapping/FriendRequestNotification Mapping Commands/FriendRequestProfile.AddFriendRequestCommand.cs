using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Data.Models.Friends;

namespace Social_Media.Core.Mapping.FriendRequestNotification_Mapping
{
    public partial class FriendRequestProfile
    {
        public void MappingAddFriendRequestNotification()
        {
            CreateMap<FriendRequest, AddFriendRequestCommand>()
                .ForMember(d => d.UserId_ThatSentFriendRequest, Opt => Opt.MapFrom(S => S.UserId))
                .ForMember(d => d.UserId_ThatReceiveFriendRequest, Opt => Opt.MapFrom(S => S.FriendUserId))
                .ReverseMap();
        }
    }
}
