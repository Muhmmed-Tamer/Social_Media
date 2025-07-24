using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Friends.Commands.Models;
using Social_Media.Core.Features.Friends.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendController : AppControllerBase
    {
        public FriendController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpGet("FriendsOFUser/{UserId:guid}")]
        public IActionResult GetFriendsOFUser(string UserId)
        {
            var Result = Mediator.Send(new GetFriendsOFUserQuery(UserId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("DeleteFriend")]
        public IActionResult DeleteFriendsOFUser(DeleteFriendCommand command)
        {

            var Result = Mediator.Send(new DeleteFriendCommand(command.Id,command.Main_UserId,command.Friend_UserId)).Result;
            return New_Result(Result);

        }
    }
}
