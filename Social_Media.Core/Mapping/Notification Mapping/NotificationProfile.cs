using AutoMapper;

namespace Social_Media.Core.Mapping.Notification_Mapping
{
    public partial class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            MappingGetAllNotificationOFUser();
        }
    }
}
