using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Services.ImplementationServices.PostServices
{
    public class TextPostServices : Services<TextPost>, ITextPostServices
    {
        public TextPostServices(ILogger<Services<TextPost>> Logger, IRepository<TextPost> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
