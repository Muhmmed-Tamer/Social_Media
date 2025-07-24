using MediatR;
using Social_Media.Core.Features.Interactions_With_Post.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Interactions_With_Post.Queries.Models
{
    public class GetInteractionWithPostQuery : IRequest<Response<List<InteractionWithPostQuery>>>
    {
        public int PostId { get; set; }
        public GetInteractionWithPostQuery() { }
        public GetInteractionWithPostQuery(int PostId)
        {
            this.PostId = PostId;
        }
    }
}
