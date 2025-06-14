using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Models;

namespace Social_Media.Core.Mapping.Post_Mapping
{
    public partial class PostProfile
    {
        public void MappingAddTextPostCommand()
        {
            CreateMap<AddTextPostCommand, TextPost>()
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Post_Content))
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId_That_Want_To_AddPost))
                .ForMember(d => d.Privacy, Opt => Opt.MapFrom(S => S.Post_Privacy))
                .ReverseMap();
        }
    }
}
