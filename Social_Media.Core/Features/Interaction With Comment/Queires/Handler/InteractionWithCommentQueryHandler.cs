using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interaction_With_Comment.Queires.Models;
using Social_Media.Core.Features.Interaction_With_Comment.Queires.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Features.Interaction_With_Comment.Queires.Handler
{
    public class InteractionWithCommentQueryHandler : ResponseHandler, IRequestHandler<GetInteractionWithCommentQuery, Response<List<InteractionWithCommentQuery>>>
    {
        private IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public InteractionWithCommentQueryHandler(IUnitOFWork UnitOFWork, ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<List<InteractionWithCommentQuery>>> Handle(GetInteractionWithCommentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Post PostIsExistOrNot = await UnitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(request.PostId);
                if (PostIsExistOrNot is null)
                {
                    return BadRequest<List<InteractionWithCommentQuery>>("Post Not Found");
                }
                Comment CommentIsExistOrNot = await UnitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(request.CommentId);
                if (CommentIsExistOrNot is null)
                {
                    return BadRequest<List<InteractionWithCommentQuery>>("Comment Not Found");
                }
                List<InteractionWithComment> AllInteractionWithComment = await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetAllInteractionOFCommentAsync(request.PostId, request.CommentId);
                List<InteractionWithCommentQuery> Mapped_InteractionWithCommentQuery = UnitOFWork.Mapper.Map<List<InteractionWithCommentQuery>>(AllInteractionWithComment);
                return OK(Mapped_InteractionWithCommentQuery);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return BadRequest<List<InteractionWithCommentQuery>>(ex.Message);
            }
        }
    }
}
