using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Interaction_With_Comment.Queires.Results
{
    public class InteractionWithCommentQuery
    {
        public InteractionType InteractBy { get; set; }
        public string UserId { get; set; }
        public InteractionWithCommentQuery()
        {

        }
        public InteractionWithCommentQuery(string UserId, InteractionType InteractBy)
        {
            this.InteractBy = InteractBy;
            this.UserId = UserId;
        }
    }
}
