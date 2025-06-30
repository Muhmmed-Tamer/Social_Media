using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Data.Models.Comments;

namespace Social_Media.Core.Mapping.Comment_Mapping
{
    public partial class CommentProfile
    {
        public void MappingGetCommentOFPostQuery()
        {
            CreateMap<Comment, CommentsOFPostQuery>()
                .ForMember(d => d.CommentId, Opt => Opt.MapFrom(S => S.Id))
                .ForMember(d => d.MakeCommentIn, Opt => Opt.MapFrom(S => S.CreatedAt))
                .ForPath(d => d.UserIdThatMakeCommentInPost, Opt => Opt.MapFrom(S => S.UserId))
                .ForMember(d => d.CommentContentThatUserMade, Opt => Opt.MapFrom(S => S.Content))
                .ReverseMap();

        }
    }
}
