using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Comments;
using Social_Media.InfraStructure.AbstractsRepositories.CommentRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.CommentRepository
{
    public class ReplyOFCommentRepository : Repository<ReplyOFComment>, IReplyOFCommentRepository
    {
        private readonly DbSet<ReplyOFComment> ReplyOFComment;
        private readonly ILogger Logger;
        public ReplyOFCommentRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.ReplyOFComment = Data.Set<ReplyOFComment>();
            this.Logger = Logger;
        }
    }
}
