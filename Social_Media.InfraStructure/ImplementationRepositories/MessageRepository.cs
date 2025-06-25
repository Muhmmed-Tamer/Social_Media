using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Chat;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.ImplementationRepositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> Message;
        public MessageRepository(Social_Media.Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.Message = Data.Set<Message>();
        }
    }
}
