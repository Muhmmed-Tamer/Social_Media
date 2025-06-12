using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Social_Media.Models;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> Message;
        public MessageRepository(Social_Media.Data.ContextData Data, ILogger<Repository<Message>> Logger) : base(Data, Logger)
        {
            this.Message = Data.Set<Message>();
        }
    }
}
