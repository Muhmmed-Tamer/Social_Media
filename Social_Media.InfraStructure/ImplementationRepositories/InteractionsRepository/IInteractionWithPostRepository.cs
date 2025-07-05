using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Interactions;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository
{
    public class InteractionWithPostRepository : Repository<InteractionWithPost>, IInteractionWithPostRepository
    {
        private readonly DbSet<InteractionWithPost> InteractionWithPost;
        private readonly ILogger Logger;
        public InteractionWithPostRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.InteractionWithPost = Data.Set<InteractionWithPost>();
            this.Logger = Logger;
        }

        public async Task<List<InteractionWithPost>> GetInteractionWithPostByPostIdAsync(int PostIdThatInteractWith)
        {
            try
            {
                return await InteractionWithPost.Where(I => I.PostId == PostIdThatInteractWith && !I.IsDeleted).Include(I => I.User).AsNoTracking().OrderByDescending(I => I.CreatedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<bool> TheUserIsInteractWithPostBeforeAsync(string UserIdThatInteractWithPost, int PostIdThatInteractWith)
        {
            try
            {
                InteractionWithPost UserIsInteractBeforeOrNot = await InteractionWithPost.Where(I => I.UserId == UserIdThatInteractWithPost && I.PostId == PostIdThatInteractWith).FirstOrDefaultAsync();
                return UserIsInteractBeforeOrNot is not null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
