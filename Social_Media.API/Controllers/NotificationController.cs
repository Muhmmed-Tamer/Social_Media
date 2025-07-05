using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Notifications.Commands.Models;
using Social_Media.Core.Features.Notifications.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NotificationController : AppControllerBase
    {
        public NotificationController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpGet("AllNotificationOFUser/{UserId:guid}")]
        public IActionResult GetAllNotificationOFUser(string UserId)
        {
            var Result = Mediator.Send(new GetNotificationOFUserQuery(UserId)).Result;
            return New_Result(Result);
        }

        [HttpGet("AllNotificationOFUser/AsPages/{UserId:guid}/{PageSize:int}/{PageNumber:int}")]
        public IActionResult GetAllNotificationOFUserAsQueryable(string UserId, int PageSize, int PageNumber)
        {
            var Result = Mediator.Send(new GetNotificationOFUserAsQueryableQuery(UserId, PageSize, PageNumber)).Result;
            return New_Result(Result);
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotification(DeleteNotificationCommand deleteNotificationCommandFRomUser)
        {
            var Result = Mediator.Send(new DeleteNotificationCommand(deleteNotificationCommandFRomUser.NotificationId, deleteNotificationCommandFRomUser.UserIdThatWantToDeleteNotification)).Result;
            return New_Result(Result);

        }
    }
}
