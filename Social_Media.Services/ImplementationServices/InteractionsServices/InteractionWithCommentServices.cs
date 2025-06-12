using Microsoft.Extensions.Logging;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Services.ImplementationServices.InteractionsServices
{
    public class InteractionWithCommentServices : Services<InteractionWithComment>, IInteractionWithCommentServices
    {
        public InteractionWithCommentServices(ILogger<Services<InteractionWithComment>> Logger, IRepository<InteractionWithComment> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override or Add New Methods Here if Needed
    }
}
