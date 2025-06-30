using Serilog;
using Social_Media.Data.Models.Chat;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.ChatRepository;
using Social_Media.Services.AbstractsServices.ChatServices;

namespace Social_Media.Services.ImplementationServices.ChatServices
{
    public class MessageMediaPathServices : Services<MessageMediaPath>, IMessageMediaPathServices
    {
        private readonly IMessageMediaPathRepository MessageMediaPathRepository;
        private readonly ILogger Logger;
        public MessageMediaPathServices(ILogger Logger, IRepository<MessageMediaPath> Repository, IMessageMediaPathRepository MessageMediaPathRepository) : base(Logger, Repository)
        {
            this.MessageMediaPathRepository = MessageMediaPathRepository;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<MessageMediaPath> MessagesMediaPaths)
        {
            try
            {
                await MessageMediaPathRepository.BulkInsertAsync(MessagesMediaPaths);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
