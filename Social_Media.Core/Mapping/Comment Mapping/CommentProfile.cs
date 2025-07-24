using AutoMapper;

namespace Social_Media.Core.Mapping.Comment_Mapping
{
    public partial class CommentProfile : Profile
    {
        public CommentProfile()
        {
            MappingAddCommentToPostCommand();

            MappingAddReplyOFCommentCommand();
            MappingReplyOFCommentQuery();
            MappingGetCommentOFPostQuery();

        }
    }
}
