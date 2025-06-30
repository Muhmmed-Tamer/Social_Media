using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Data.Models.Comments;

namespace Social_Media.Core.Mapping.Comment_Mapping
{
    public partial class CommentProfile
    {
        public void MappingReplyOFCommentQuery()
        {
            CreateMap<ReplyOFComment, ReplyOFCommentQuery>()
                .ForMember(d => d.UserIdThatWriteAReplyOFComment, Opt => Opt.MapFrom(S => S.UserIdThatWriteAReplyOFComment))
                .ForMember(d => d.UserIdThatWriteAComment, Opt => Opt.MapFrom(S => S.UserIdThatWriteAComment))
                .ForMember(d => d.CreatedAt, Opt => Opt.MapFrom(S => S.CreatedAt))
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Content))
                .ReverseMap();
        }
    }
}
