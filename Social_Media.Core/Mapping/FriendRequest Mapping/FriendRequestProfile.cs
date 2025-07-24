using AutoMapper;

namespace Social_Media.Core.Mapping.FriendRequestNotification_Mapping
{
    public partial class FriendRequestProfile : Profile
    {
        public FriendRequestProfile()
        {
            MappingAddFriendRequestNotification();
        }
    }
}
