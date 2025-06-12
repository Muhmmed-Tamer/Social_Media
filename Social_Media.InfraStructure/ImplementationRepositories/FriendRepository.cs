using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Models;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class FriendRepository : Repository<Friend>, IFriendRepository
    {
        private readonly DbSet<Friend> Friend;
        public FriendRepository(Social_Media.Data.ContextData Data, ILogger<Repository<Friend>> Logger) : base(Data, Logger)
        {
            this.Friend = Data.Set<Friend>();
        }
    }
}
