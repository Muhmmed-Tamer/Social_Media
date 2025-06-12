using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository
{
    public class InteractionWithStoryRepository : Repository<InteractionWithStory>, IInteractionWithStoryRepository
    {
        private readonly DbSet<InteractionWithStory> InteractionWithStory;
        public InteractionWithStoryRepository(Social_Media.Data.ContextData Data, ILogger<Repository<InteractionWithStory>> Logger) : base(Data, Logger)
        {
            this.InteractionWithStory = Data.Set<InteractionWithStory>();
        }
    }
}
