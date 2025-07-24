using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class AddAudioMessageCommand : IRequest<Response<string>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public IFormFile Audio { get; set; }
        public AddAudioMessageCommand()
        {

        }
        public AddAudioMessageCommand(string SenderId, string ReceiverId, IFormFile Audio)
        {
            this.ReceiverId = ReceiverId;
            this.SenderId = SenderId;
            this.Audio = Audio;
        }
    }
}
