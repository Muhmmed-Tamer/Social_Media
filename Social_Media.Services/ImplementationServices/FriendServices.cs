using Microsoft.Extensions.Logging;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class FriendServices : Services<Friend>, IFriendServices
    {
        public FriendServices(ILogger<Services<Friend>> Logger, IRepository<Friend> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
