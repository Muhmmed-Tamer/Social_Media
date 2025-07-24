using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Mapping.Post_Mapping
{
    public partial class PostProfile
    {
        public void MappingAddTextPostCommand()
        {
            CreateMap<AddTextPostCommand, Post>()
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Post_Content))
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId_That_Want_To_AddPost))
                .ForMember(d => d.Privacy, Opt => Opt.MapFrom(S => S.Post_Privacy))
                .ForMember(d => d.PostType, Opt => Opt.MapFrom(_ => PostType.Text))
                .ReverseMap();
        }
        public void MappingAddTextPostRealTimeCommand()
        {

        }
    }
}
