using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Chat;
using Social_Media.InfraStructure.AbstractsRepositories.ChatRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.ChatRepository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> Message;
        private readonly ILogger Logger;
        public MessageRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            Message = Data.Set<Message>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<Message> Messages)
        {
            try
            {
                await Message.BulkInsertAsync(Messages);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Message>> GetMessagesBetweenTwoUsersAsync(string SenderId, string ReceiverId)
        {
            try
            {
                return await Message.Where(M => M.SenderId == SenderId & M.ReceiverId == ReceiverId).Include(MP => MP.MessageMediaPaths).OrderByDescending(M => M.CreatedAt).ToListAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
