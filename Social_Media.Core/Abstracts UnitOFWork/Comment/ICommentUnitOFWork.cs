using Social_Media.Services.AbstractsServices.CommentServices;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface ICommentUnitOFWork
    {
        public IReplyOFCommentServices ReplyOFCommentService { get; }
        public ICommentServices CommentServices { get; }
    }
}
