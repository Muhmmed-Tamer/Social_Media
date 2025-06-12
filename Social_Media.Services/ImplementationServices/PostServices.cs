using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class PostServices : Services<Post>, IPostServices
    {
        public PostServices(ILogger<Services<Post>> Logger, IRepository<Post> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
