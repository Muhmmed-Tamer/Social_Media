using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using Social_Media.Core.Features.Interaction_With_Comment.Queires.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InteractionWithCommentController : AppControllerBase
    {
        public InteractionWithCommentController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("Add")]
        public IActionResult AddInteractionWithPost(AddInteractionWithCommentCommand addInteractionWithCommentCommandFromUser)
        {
            var Result = Mediator.Send(new AddInteractionWithCommentCommand(addInteractionWithCommentCommandFromUser.UserId, addInteractionWithCommentCommandFromUser.CommentId, addInteractionWithCommentCommandFromUser.PostId, addInteractionWithCommentCommandFromUser.InteractBy)).Result;
            return New_Result(Result);
        }
        [HttpGet("Get/{PostId:int}/{CommentId:int}")]
        public IActionResult GetInteractionWithPost(int PostId, int CommentId)
        {
            var Result = Mediator.Send(new GetInteractionWithCommentQuery(PostId, CommentId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteInteractionWithComment(DeleteInteractionWithCommentCommand command)
        {
            var Result = Mediator.Send(new DeleteInteractionWithCommentCommand(command.Id, command.CommentId)).Result;
            return New_Result(Result);
        }
    }
}
