using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly DbSet<Comment> Comment;
        public CommentRepository(Social_Media.Data.ContextData Data, ILogger<Repository<Comment>> Logger) : base(Data, Logger)
        {
            this.Comment = Data.Set<Comment>();
        }

    }
}
