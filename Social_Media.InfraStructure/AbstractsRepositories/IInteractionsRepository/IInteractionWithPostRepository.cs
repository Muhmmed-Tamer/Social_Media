using Social_Media.Data.Models.Interactions;

namespace Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository
{
    public interface IInteractionWithPostRepository : IRepository<InteractionWithPost>
    {
        Task<bool> TheUserIsInteractWithPostBeforeAsync(string UserIdThatInteractWithPost, int PostIdThatInteractWith);
        Task<List<InteractionWithPost>> GetInteractionWithPostByPostIdAsync(int PostIdThatInteractWith);
    }
}
