using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Services.ImplementationServices.InteractionsServices
{
    public class InteractionWithStoryServices : Services<InteractionWithStory>, IInteractionWithStoryServices
    {
        public InteractionWithStoryServices(ILogger Logger, IRepository<InteractionWithStory> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
