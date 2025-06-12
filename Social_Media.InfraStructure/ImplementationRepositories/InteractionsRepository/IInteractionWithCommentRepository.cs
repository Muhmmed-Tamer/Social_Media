using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository
{
    public class InteractionWithCommentRepository : Repository<InteractionWithComment>, IInteractionWithCommentRepository
    {
        private readonly DbSet<InteractionWithComment> InteractionWithComment;
        public InteractionWithCommentRepository(Social_Media.Data.ContextData Data, ILogger<Repository<InteractionWithComment>> Logger) : base(Data, Logger)
        {
            this.InteractionWithComment = Data.Set<InteractionWithComment>();
        }
    }
}
