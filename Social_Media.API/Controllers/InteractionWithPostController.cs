using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using Social_Media.Core.Features.Interactions_With_Post.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InteractionWithPostController : AppControllerBase
    {
        public InteractionWithPostController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("AddInteractionWithPost")]
        public IActionResult Add(AddInteractionWithPostCommand addInteractionWithPostCommandFromUser)
        {
            var Result = Mediator.Send(new AddInteractionWithPostCommand(addInteractionWithPostCommandFromUser.UserIdThatInteractWithPost, addInteractionWithPostCommandFromUser.InteractionBy, addInteractionWithPostCommandFromUser.PostIdThatInteractWith)).Result;
            return New_Result(Result);
        }
        [HttpGet("GetAllInteractionsByPostId/{PostId}")]
        public IActionResult GetAllInteractionsByPostId(int PostId)
        {
            var Result = Mediator.Send(new GetInteractionWithPostQuery(PostId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(DeleteInteractionWithPostCommand command)
        {
            var Result = Mediator.Send(new DeleteInteractionWithPostCommand(command.Id, command.PostId)).Result;

            return New_Result(Result);
        }
    }
}
