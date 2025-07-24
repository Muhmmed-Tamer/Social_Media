using AutoMapper;

namespace Social_Media.Core.Mapping.ConfirmFriendRequest_Notification_Mapping
{
    public partial class ConfirmFriendRequestProfile : Profile
    {
        public ConfirmFriendRequestProfile()
        {
            MappingConfirmFriendRequestCommand();
        }
    }
}
