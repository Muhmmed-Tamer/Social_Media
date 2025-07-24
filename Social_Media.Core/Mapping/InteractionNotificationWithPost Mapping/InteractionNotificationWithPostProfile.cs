using AutoMapper;

namespace Social_Media.Core.Mapping.InteractionNotificationWithPost_Mapping
{
    public partial class InteractionNotificationWithPostProfile : Profile
    {
        public InteractionNotificationWithPostProfile()
        {
            MappingAddInteractionNotificationWithPost1();
            MappingAddInteractionNotificationWithPost2();
            MappingGetInteractionWithPostQuery();
        }
    }
}
