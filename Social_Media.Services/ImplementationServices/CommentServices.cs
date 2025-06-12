using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class CommentServices : Services<Comment>, ICommentServices
    {
        public CommentServices(ILogger<Services<Comment>> Logger, IRepository<Comment> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
