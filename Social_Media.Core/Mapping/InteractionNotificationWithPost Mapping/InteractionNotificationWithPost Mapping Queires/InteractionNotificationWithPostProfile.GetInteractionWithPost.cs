using Social_Media.Core.Features.Interactions_With_Post.Queries.Results;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.Core.Mapping.InteractionNotificationWithPost_Mapping
{
    public partial class InteractionNotificationWithPostProfile
    {
        public void MappingGetInteractionWithPostQuery()
        {
            CreateMap<InteractionWithPost, InteractionWithPostQuery>()
                .ForMember(d => d.InteractionType, Opt => Opt.MapFrom(S => S.InteractionBy))
                .ForPath(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId))
                .ReverseMap();
        }
    }
}
