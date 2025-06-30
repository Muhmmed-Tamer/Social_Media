using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models
{
    public class AddFriendRequestCommand : IRequest<Response<string>>
    {
        public string UserId_ThatSentFriendRequest { get; set; }
        public string UserId_ThatReceiveFriendRequest { get; set; }
        public AddFriendRequestCommand()
        {

        }
        public AddFriendRequestCommand(string UserId_ThatSentFriendRequest, string UserId_ThatReceiveFriendRequest)
        {
            this.UserId_ThatSentFriendRequest = UserId_ThatSentFriendRequest;
            this.UserId_ThatReceiveFriendRequest = UserId_ThatReceiveFriendRequest;
        }
    }
}
