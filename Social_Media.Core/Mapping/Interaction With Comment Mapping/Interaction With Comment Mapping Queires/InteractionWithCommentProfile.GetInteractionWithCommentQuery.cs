using Social_Media.Core.Features.Interaction_With_Comment.Queires.Results;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.Core.Mapping.Interaction_With_Comment_Mapping
{
    public partial class InteractionWithCommentProfile
    {
        public void MappingGetInteractionWithCommentQuery()
        {
            CreateMap<InteractionWithComment, InteractionWithCommentQuery>()
                .ForMember(d => d.InteractBy, Opt => Opt.MapFrom(S => S.InteractionBy))
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId))
                .ReverseMap();
        }
    }
}
