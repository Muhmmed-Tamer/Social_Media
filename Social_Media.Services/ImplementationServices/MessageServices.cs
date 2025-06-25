using Serilog;
using Social_Media.Data.Models.Chat;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class MessageServices : Services<Message>, IMessageServices
    {
        public MessageServices(ILogger Logger, IRepository<Message> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
