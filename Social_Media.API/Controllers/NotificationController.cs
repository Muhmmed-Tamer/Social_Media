using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Notifications.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
