using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.PostsRepository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ILogger Logger;
        private readonly DbSet<Post> Post;
        public PostRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            Post = Data.Set<Post>();
            this.Logger = Logger;
        }

        public async Task<List<Post>> GetPostsOFUserAsync(string UserId)
        {
            try
            {
                return await Post.Where(P => P.UserId == UserId && !P.IsDeleted).Include(P => P.ImageOrVideo_Paths.Where(P => P.IsDeleted == false))?.OrderByDescending(P => P.CreatedDate).ToListAsync()!;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting posts of user {UserId}", UserId);
                throw;
            }
        }

        public Task<Post> GetPostWithImageOrVideoPathsById(int PostId)
        {
            try
            {
                return Post.Where(P => P.Id == PostId && !P.IsDeleted).Include(P => P.ImageOrVideo_Paths).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting post with ID {PostId}", PostId);
                throw;
            }
        }
    }
}
