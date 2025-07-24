using MediatR;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interactions_With_Post.Queries.Models;
using Social_Media.Core.Features.Interactions_With_Post.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Posts;

namespace Social_Media.Core.Features.Interactions_With_Post.Queries.Handlers
{
    public class InteractionWithPostHandler : ResponseHandler, IRequestHandler<GetInteractionWithPostQuery, Response<List<InteractionWithPostQuery>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly Serilog.ILogger Logger;
        public InteractionWithPostHandler(Serilog.ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<List<InteractionWithPostQuery>>> Handle(GetInteractionWithPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Check If Post Is Exist Or Not
                Post PostIsExistOrNot = await UnitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(request.PostId);
                if (PostIsExistOrNot is null)
                {
                    return BadRequest<List<InteractionWithPostQuery>>("Post Not Found");
                }
                List<InteractionWithPost> InteractionWithPost = await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.GetInteractionWithPostByPostIdAsync(request.PostId);
                List<InteractionWithPostQuery> Mapped_InteractionWithPostQuery = UnitOFWork.Mapper.Map<List<InteractionWithPostQuery>>(InteractionWithPost);
                return OK(Mapped_InteractionWithPostQuery);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error while getting interactions with post");
                return BadRequest<List<InteractionWithPostQuery>>("An error occurred while fetching interactions.");
            }
        }
    }
}
