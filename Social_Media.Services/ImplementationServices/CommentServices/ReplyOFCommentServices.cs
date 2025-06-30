using Serilog;
using Social_Media.Data.Models.Comments;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices.CommentServices;

namespace Social_Media.Services.ImplementationServices.CommentServices
{
    public class ReplyOFCommentServices : Services<ReplyOFComment>, IReplyOFCommentServices
    {
        public ReplyOFCommentServices(ILogger Logger, IRepository<ReplyOFComment> Repository) : base(Logger, Repository)
        {
        }
    }
}
