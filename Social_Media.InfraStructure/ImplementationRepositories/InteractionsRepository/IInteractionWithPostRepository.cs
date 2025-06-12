using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository
{
    public class InteractionWithPostRepository : Repository<InteractionWithPost>, IInteractionWithPostRepository
    {
        private readonly DbSet<InteractionWithPost> InteractionWithPost;
        public InteractionWithPostRepository(Social_Media.Data.ContextData Data, ILogger<Repository<InteractionWithPost>> Logger) : base(Data, Logger)
        {
            this.InteractionWithPost = Data.Set<InteractionWithPost>();
        }
    }
}
