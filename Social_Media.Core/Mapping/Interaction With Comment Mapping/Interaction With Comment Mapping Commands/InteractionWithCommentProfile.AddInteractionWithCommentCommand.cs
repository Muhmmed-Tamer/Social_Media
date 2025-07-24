using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.Core.Mapping.Interaction_With_Comment_Mapping
{
    public partial class InteractionWithCommentProfile
    {
        public void MappingAddInteractionWithCommentCommand()
        {
            CreateMap<AddInteractionWithCommentCommand, InteractionWithComment>()
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId))
                .ForMember(d => d.CommentId, Opt => Opt.MapFrom(S => S.CommentId))
                .ForMember(d => d.InteractionBy, Opt => Opt.MapFrom(S => S.InteractBy))
                .ForMember(d => d.PostId, Opt => Opt.MapFrom(S => S.PostId))
                .ReverseMap();
        }
    }
}
