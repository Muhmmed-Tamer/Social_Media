using Social_Media.Data.Models.Chat;

namespace Social_Media.InfraStructure.AbstractsRepositories.ChatRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetMessagesBetweenTwoUsersAsync(string SenderId, string ReceiverId);
        Task BulkInsertAsync(List<Message> Messages);
    }
}
