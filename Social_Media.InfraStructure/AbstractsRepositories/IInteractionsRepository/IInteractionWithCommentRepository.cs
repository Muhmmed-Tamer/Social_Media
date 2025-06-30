using Social_Media.Data.Models.Interactions;

namespace Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository
{
    public interface IInteractionWithCommentRepository : IRepository<InteractionWithComment>
    {
        Task<bool> UserIsInteractWithCommentBeforeAsync(string UserId, int CommentId, int PostId);
        Task<List<InteractionWithComment>> GetAllInteractionOFCommentAsync(int PostId, int CommentId);
    }
}
