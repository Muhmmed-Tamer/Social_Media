using AutoMapper;

namespace Social_Media.Core.Mapping.NewMessageSentNotification_Mapping
{
    public partial class NewMessageNotificationProfile : Profile
    {
        public NewMessageNotificationProfile()
        {
            MappingNewAudioMessageNotification();
            MappingNewMediaMessageNotification();
            MappingNewMessageNotification();
        }
    }
}
