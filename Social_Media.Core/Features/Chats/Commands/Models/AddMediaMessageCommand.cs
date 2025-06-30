using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class AddMediaMessageCommand : IRequest<Response<string>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public List<IFormFile> Medias { get; set; }
        public AddMediaMessageCommand()
        {

        }
        public AddMediaMessageCommand(string SenderId, string ReceiverId, List<IFormFile> Medias)
        {
            this.SenderId = SenderId;
            this.ReceiverId = ReceiverId;
            this.Medias = Medias;
        }
    }
}
