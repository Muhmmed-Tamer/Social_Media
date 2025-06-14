using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;
using Social_Media.Models;

namespace Social_Media.InfraStructure.ImplementationRepositories.PostsRepository
{
    public class TextPostRepository : Repository<TextPost>, ITextPostRepository
    {
        private readonly DbSet<TextPost> Post;
        public TextPostRepository(Data.ContextData Data, ILogger<Repository<TextPost>> Logger) : base(Data, Logger)
        {
            Post = Data.Set<TextPost>();
        }
    }
}
