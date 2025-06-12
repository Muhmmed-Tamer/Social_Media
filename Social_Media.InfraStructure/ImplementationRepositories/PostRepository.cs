using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Models;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly DbSet<Post> Post;
        public PostRepository(Social_Media.Data.ContextData Data, ILogger<Repository<Post>> Logger) : base(Data, Logger)
        {
            this.Post = Data.Set<Post>();
        }
    }
}
