using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;
using Social_Media.Services.AbstractsServices.InteractionsServices;

namespace Social_Media.Services.ImplementationServices.InteractionsServices
{
    public class InteractionWithCommentServices : Services<InteractionWithComment>, IInteractionWithCommentServices
    {
        private readonly IInteractionWithCommentRepository InteractionWithCommentRepository;
        private readonly ILogger Logger;
        public InteractionWithCommentServices(ILogger Logger, IRepository<InteractionWithComment> Repository, IInteractionWithCommentRepository InteractionWithCommentRepository) : base(Logger, Repository)
        {
            this.InteractionWithCommentRepository = InteractionWithCommentRepository;
            this.Logger = Logger;
        }

        public async Task<List<InteractionWithComment>> GetAllInteractionOFCommentAsync(int PostId, int CommentId)
        {
            try
            {
                return await InteractionWithCommentRepository.GetAllInteractionOFCommentAsync(PostId, CommentId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<bool> UserIsInteractWithCommentBeforeAsync(string UserId, int CommentId, int PostId)
        {
            try
            {
                return await InteractionWithCommentRepository.UserIsInteractWithCommentBeforeAsync(UserId, CommentId, PostId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in UserIsInteractWithCommentBeforeAsync");
                throw;
            }
        }
        //You Can Override or Add New Methods Here if Needed
    }
}
