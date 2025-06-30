using AutoMapper;

namespace Social_Media.Core.Mapping.InteractionNotificationWithCommentMapping
{
    public partial class InteractionNotificationWithCommentProfile : Profile
    {
        public InteractionNotificationWithCommentProfile()
        {
            MappingAddInteractionWithCommentCommand();
        }
    }
}
