using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.ChatServices;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class ChatUnitOFWork : IChatUnitOFWork
    {
        public ChatUnitOFWork(IMessageMediaPathServices MessageMediaPathService, IMessageServices MessageServices)
        {
            this.MessageMediaPathService = MessageMediaPathService;
            this.MessageServices = MessageServices;
        }

        public IMessageMediaPathServices MessageMediaPathService { get; }

        public IMessageServices MessageServices { get; }
    }
}
