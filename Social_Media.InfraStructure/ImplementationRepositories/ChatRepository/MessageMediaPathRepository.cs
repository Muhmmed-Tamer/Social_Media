using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Chat;
using Social_Media.InfraStructure.AbstractsRepositories.ChatRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.ChatRepository
{
    public class MessageMediaPathRepository : Repository<MessageMediaPath>, IMessageMediaPathRepository
    {
        private readonly DbSet<MessageMediaPath> MessageMediaPth;
        private readonly ILogger Logger;
        public MessageMediaPathRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            MessageMediaPth = Data.Set<MessageMediaPath>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<MessageMediaPath> MessagesMediaPaths)
        {
            try
            {
                await MessageMediaPth.BulkInsertAsync(MessagesMediaPaths);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
