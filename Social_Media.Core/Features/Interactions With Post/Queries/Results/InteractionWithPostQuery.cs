using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Interactions_With_Post.Queries.Results
{
    public class InteractionWithPostQuery
    {
        public string UserId { get; set; }
        public InteractionType InteractionType { get; set; }
    }
}
