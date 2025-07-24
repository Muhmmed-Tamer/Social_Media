using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Queires.Models;
using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Comments;

namespace Social_Media.Core.Features.Comments.Queires.Handlers
{
    public class CommentQueryHandler : ResponseHandler, IRequestHandler<GetCommentsOFPostQuery, Response<List<CommentsOFPostQuery>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public CommentQueryHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<List<CommentsOFPostQuery>>> Handle(GetCommentsOFPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Comment> CommentsOFPost = await UnitOFWork.CommentUnitOFWork.CommentServices.GetCommentsByPostIdAsync(request.PostId);
                var Result = CommentsOFPost.Select(Comment => new CommentsOFPostQuery()
                {
                    CommentContentThatUserMade = Comment.Content,
                    CommentId = Comment.Id,
                    MakeCommentIn = Comment.CreatedAt,
                    UserIdThatMakeCommentInPost = Comment.UserId,
                    Replies = Comment?.ReplyOFComments?.Select(Reply => new ReplyOFCommentQuery()
                    {
                        Content = Reply.Content,
                        CreatedAt = Reply.CreatedAt,
                        UserIdThatWriteAComment = Reply.UserIdThatWriteAComment,
                        UserIdThatWriteAReplyOFComment = Reply.UserIdThatWriteAReplyOFComment
                    }).ToList()
                }).ToList();
                return OK(Result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<List<CommentsOFPostQuery>>(ex.Message);
            }
        }
    }
}
