using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Services.ImplementationServices.InteractionsServices
{
    public class InteractionWithPostServices : Services<InteractionWithPost>, IInteractionWithPostServices
    {
        public InteractionWithPostServices(ILogger<Services<InteractionWithPost>> Logger, IRepository<InteractionWithPost> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
