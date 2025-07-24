using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository
{
    public class InteractionWithCommentRepository : Repository<InteractionWithComment>, IInteractionWithCommentRepository
    {
        private readonly DbSet<InteractionWithComment> InteractionWithComment;
        private readonly ILogger Logger;
        public InteractionWithCommentRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.InteractionWithComment = Data.Set<InteractionWithComment>();
            this.Logger = Logger;
        }

        public async Task<List<InteractionWithComment>> GetAllInteractionOFCommentAsync(int PostId, int CommentId)
        {
            try
            {
                return await InteractionWithComment.Where(I => I.PostId == PostId && I.CommentId == CommentId && !I.IsDeleted).Include(U => U.User).AsNoTracking().ToListAsync();
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
                InteractionWithComment InteractionWithCommentByUser = await InteractionWithComment.Where(C => C.UserId == UserId && C.CommentId == CommentId && C.PostId == PostId).AsNoTracking().FirstOrDefaultAsync();
                if (InteractionWithCommentByUser is not null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in UserIsInteractWithCommentBeforeAsync");
                throw;
            }
        }
    }
}
