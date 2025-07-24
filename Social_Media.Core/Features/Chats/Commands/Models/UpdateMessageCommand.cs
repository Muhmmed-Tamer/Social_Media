using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class UpdateMessageCommand : IRequest<Response<string>>
    {

        public int MessageId { get; set; }
        public string New_Content { get; set; }
        public string UserIdWhoWantToUpdate { get; set; }

        public UpdateMessageCommand() { }

        public UpdateMessageCommand(int MessageId, string New_Content, string UserIdWhoWantToUpdate)
        {
            this.UserIdWhoWantToUpdate = UserIdWhoWantToUpdate;
            this.New_Content = New_Content;
            this.MessageId = MessageId;
        }
    }
}
