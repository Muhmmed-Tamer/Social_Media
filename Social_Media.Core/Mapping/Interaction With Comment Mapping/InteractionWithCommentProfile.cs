using AutoMapper;

namespace Social_Media.Core.Mapping.Interaction_With_Comment_Mapping
{
    public partial class InteractionWithCommentProfile : Profile
    {
        public InteractionWithCommentProfile()
        {
            MappingAddInteractionWithCommentCommand();
            MappingGetInteractionWithCommentQuery();
        }
    }
}
