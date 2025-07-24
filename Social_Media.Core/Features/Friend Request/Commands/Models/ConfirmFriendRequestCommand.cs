using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models
{
    public class ConfirmFriendRequestCommand : IRequest<Response<string>>
    {
        public ConfirmFriendRequestCommand()
        {

        }
        public string UserIdThatSentFriendRequest { get; set; }
        public string FriendUserIdThatConfirmFriendRequest { get; set; }
        public ConfirmFriendRequestCommand(string FriendUserIdThatConfirmFriendRequest, string UserIdThatSentFriendRequest)
        {
            this.FriendUserIdThatConfirmFriendRequest = FriendUserIdThatConfirmFriendRequest;
            this.UserIdThatSentFriendRequest = UserIdThatSentFriendRequest;
        }
    }
}
