using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FriendRequestController : AppControllerBase
    {
        public FriendRequestController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("Add_FriendRequest")]
        public IActionResult Add([FromBody] AddFriendRequestCommand addFriendRequestCommandFromUser)
        {
            var Result = Mediator.Send(new AddFriendRequestCommand(addFriendRequestCommandFromUser.UserId_ThatSentFriendRequest, addFriendRequestCommandFromUser.UserId_ThatReceiveFriendRequest)).Result;
            return New_Result(Result);
        }
        [HttpPut("ConfirmFriendRequest")]
        public IActionResult Update([FromBody] ConfirmFriendRequestCommand confirmFriendRequestCommandFromUser)
        {
            var Result = Mediator.Send(new ConfirmFriendRequestCommand(confirmFriendRequestCommandFromUser.FriendUserIdThatConfirmFriendRequest, confirmFriendRequestCommandFromUser.UserIdThatSentFriendRequest)).Result;
            return New_Result(Result);
        }
        [HttpPut("Reject/FriendRequest")]
        public IActionResult UpdateFriendRequest([FromBody] RejectFriendRequestCommand rejectFriendRequestCommandFromUser)
        {
            var Result = Mediator.Send(new RejectFriendRequestCommand(rejectFriendRequestCommandFromUser.FriendUserIdThatConfirmFriendRequest, rejectFriendRequestCommandFromUser.UserIdThatSentFriendRequest)).Result;
            return New_Result(Result);
        }
    }
}
