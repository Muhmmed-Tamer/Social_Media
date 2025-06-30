using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class AddTextMessageCommand : IRequest<Response<string>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public AddTextMessageCommand() { }

        public AddTextMessageCommand(string SenderId, string ReceiverId, string Content)
        {
            this.SenderId = SenderId;
            this.ReceiverId = ReceiverId;
            this.Content = Content;
        }
    }
}
