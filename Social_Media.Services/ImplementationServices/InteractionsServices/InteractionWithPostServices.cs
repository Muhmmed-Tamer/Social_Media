using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Services.ImplementationServices.InteractionsServices
{
    public class InteractionWithPostServices : Services<InteractionWithPost>, IInteractionWithPostServices
    {
        private readonly IInteractionWithPostRepository InteractionWithPostRepository;
        private readonly ILogger Logger;
        public InteractionWithPostServices(ILogger Logger, IRepository<InteractionWithPost> Repository, IInteractionWithPostRepository InteractionWithPostRepository) : base(Logger, Repository)
        {
            this.InteractionWithPostRepository = InteractionWithPostRepository;
            this.Logger = Logger;
        }

        public Task<List<InteractionWithPost>> GetInteractionWithPostByPostIdAsync(int PostIdThatInteractWith)
        {
            try
            {
                return InteractionWithPostRepository.GetInteractionWithPostByPostIdAsync(PostIdThatInteractWith);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public Task<bool> TheUserIsInteractWithPostBeforeAsync(string UserIdThatInteractWithPost, int PostIdThatInteractWith)
        {
            try
            {
                return InteractionWithPostRepository.TheUserIsInteractWithPostBeforeAsync(UserIdThatInteractWithPost, PostIdThatInteractWith);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
