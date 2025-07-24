using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Comments;
using Social_Media.InfraStructure.AbstractsRepositories.CommentRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.CommentRepository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly DbSet<Comment> Comment;
        private readonly ILogger Logger;
        public CommentRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            Comment = Data.Set<Comment>();
            this.Logger = Logger;
        }

        public Task<bool> CommentIsExistInAllCommentsOfPost(List<Comment> AllComments, int CommentId)
        {
            try
            {
                if (AllComments.Where(C => C.Id == CommentId).FirstOrDefault() is not null)
                {
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error checking if comment exists in all comments of post");
                throw;
            }
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int PostId)
        {
            try
            {
                return await Comment.Where(c => c.PostId == PostId && !c.IsDeleted).Include(R => R.ReplyOFComments.Where(R => !R.IsDeleted)).Include(C => C.User).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error retrieving comments for post ID {PostId}", PostId);
                throw;
            }
        }

        public async Task<int> GetPostIdByCommentId(int CommentId)
        {
            try
            {
                return await Comment.Where(C => C.Id == CommentId).Select(C => C.PostId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
