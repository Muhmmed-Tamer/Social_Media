using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Queires.Models;
using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Posts;

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
                Post PostIsFoundOrNot = await UnitOFWork.PostServices.GetByIdAsync(request.PostId);
                if (PostIsFoundOrNot == null)
                {
                    return BadRequest<List<CommentsOFPostQuery>>($"Post with ID {request.PostId} Not found.");
                }
                List<Comment> CommentsOFPost = await UnitOFWork.CommentServices.GetCommentsByPostIdAsync(request.PostId);
                List<CommentsOFPostQuery> Mapped_CommentsOFPostQuery = UnitOFWork.Mapper.Map<List<CommentsOFPostQuery>>(CommentsOFPost);
                return OK(Mapped_CommentsOFPostQuery);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<List<CommentsOFPostQuery>>(ex.Message);
            }
        }
    }
}
