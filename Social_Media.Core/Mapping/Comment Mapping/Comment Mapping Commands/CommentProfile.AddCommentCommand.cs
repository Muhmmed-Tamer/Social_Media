using Social_Media.Data.Models.Comments;

namespace Social_Media.Core.Mapping.Comment_Mapping
{
    public partial class CommentProfile
    {
        public void MappingAddCommentToPostCommand()
        {
            CreateMap<Features.Comments.Commands.Models.AddCommentToPostCommand, Comment>()
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Comment_Content))
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.UserId))
                .ForMember(d => d.PostId, Opt => Opt.MapFrom(S => S.PostId))
                .ReverseMap();
        }
    }
}
