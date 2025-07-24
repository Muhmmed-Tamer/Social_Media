using Social_Media.Data.Models.Chat;

namespace Social_Media.InfraStructure.AbstractsRepositories.ChatRepository
{
    public interface IMessageMediaPathRepository : IRepository<MessageMediaPath>
    {
        Task BulkInsertAsync(List<MessageMediaPath> MessagesMediaPaths);
    }
}
