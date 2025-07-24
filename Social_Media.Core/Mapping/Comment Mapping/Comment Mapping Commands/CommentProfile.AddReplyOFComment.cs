using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Data.Models.Comments;

namespace Social_Media.Core.Mapping.Comment_Mapping
{
    public partial class CommentProfile
    {
        public void MappingAddReplyOFCommentCommand()
        {
            CreateMap<AddReplyOFCommentCommand, ReplyOFComment>()
                .ForMember(d => d.UserIdThatWriteAReplyOFComment, Opt => Opt.MapFrom(S => S.UserIdThatWriteAReplyOFComment))
                .ForMember(d => d.UserIdThatWriteAComment, Opt => Opt.MapFrom(S => S.UserIdThatWriteAComment))
                .ForMember(d => d.CommentId, Opt => Opt.MapFrom(S => S.CommentId))
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Content))
                .ReverseMap();
        }
    }
}
