using Social_Media.Core.Features.Posts.Queries.Results;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Mapping.Post_Mapping
{
    public partial class PostProfile
    {

        public void MappingGetTextPostQuery()
        {
            CreateMap<Post, PostsOFUserQuery>()
                .ForMember(d => d.PostId, Opt => Opt.MapFrom(S => S.Id))
                .ForMember(d => d.CreatedAt, Opt => Opt.MapFrom(S => S.CreatedDate))
                .ForMember(d => d.Privacy, Opt => Opt.MapFrom(S => S.Privacy))
                .ForMember(d => d.PostType, Opt => Opt.MapFrom(S => S.PostType))
                .ForMember(d => d.PostContent, Opt => Opt.MapFrom(S => S.Content))
                .ForMember(d => d.PostCaption, Opt => Opt.MapFrom(S => S.Caption))
                .ForMember(d => d.ImageOrVideoPaths, Opt => Opt.MapFrom(S => S.ImageOrVideo_Paths.Select(P => P.Image_Or_VideoPath).ToList()))
                .ReverseMap();
        }
    }
}
