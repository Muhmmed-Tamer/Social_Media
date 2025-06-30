using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.Core.Mapping.InteractionWithPost_Mapping
{
    public partial class InteractionWithPostProfile
    {
        public void MappingAddInteractionWithPost()
        {
            CreateMap<AddInteractionWithPostCommand, InteractionWithPost>()
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserIdThatInteractWithPost))
                .ForMember(d => d.PostId, Opt => Opt.MapFrom(S => S.PostIdThatInteractWith))
                .ForMember(d => d.InteractionBy, Opt => Opt.MapFrom(S => S.InteractionBy))
                .ReverseMap();
        }
    }
}
