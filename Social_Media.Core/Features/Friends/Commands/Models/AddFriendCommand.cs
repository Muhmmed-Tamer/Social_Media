using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Friends.Commands.Models
{
    public class AddFriendCommand : IRequest<Response<string>>
    {
        public string UserId_ThatSendRequest { get; set; }
        public string UserId_ThatReceiveRequest { get; set; }
        public AddFriendCommand()
        {

        }
        public AddFriendCommand(string UserId_ThatSendRequest, string UserId_ThatReceiveRequest)
        {
            this.UserId_ThatSendRequest = UserId_ThatSendRequest;
            this.UserId_ThatReceiveRequest = UserId_ThatReceiveRequest;
        }
    }
}
