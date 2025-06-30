using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.CommentServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class CommentUnitOFWork : ICommentUnitOFWork
    {
        public CommentUnitOFWork(IReplyOFCommentServices ReplyOFCommentService, ICommentServices CommentServices)
        {
            this.ReplyOFCommentService = ReplyOFCommentService;
            this.CommentServices = CommentServices;
        }

        public IReplyOFCommentServices ReplyOFCommentService { get; }

        public ICommentServices CommentServices { get; }
    }
}
