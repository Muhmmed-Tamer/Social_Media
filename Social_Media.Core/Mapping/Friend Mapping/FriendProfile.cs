using AutoMapper;

namespace Social_Media.Core.Mapping.Friend_Mapping
{
    public partial class FriendProfile : Profile
    {
        public FriendProfile()
        {
            MappingAddFriendCommandFromFriendRequestNotification();
        }
    }
}
