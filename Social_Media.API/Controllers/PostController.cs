using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Posts.Commands.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : AppControllerBase
    {
        private readonly ILogger<PostController> Logger;
        public PostController(IMediator Mediator, ILogger<PostController> logger) : base(Mediator)
        {
            Logger = logger;
        }
        [HttpPost("Add_TextPost")]
        public async Task<IActionResult> Add([FromForm] AddTextPostCommand addTextPostCommandFromUser)
        {
            try
            {
                var Result = await Mediator.Send(new AddTextPostCommand(addTextPostCommandFromUser.Post_Content, addTextPostCommandFromUser.UserId_That_Want_To_AddPost, addTextPostCommandFromUser.Post_Privacy));
                return New_Result(Result);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message, "From Add Post ");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Add/ImageOrVideoPost")]
        public async Task<IActionResult> Add([FromForm] AddImageOrVideoPostCommand addImageOrVideoPostCommandFromUser)
        {
            try
            {
                var Result = await Mediator.Send(new AddImageOrVideoPostCommand(addImageOrVideoPostCommandFromUser.Post_Title, addImageOrVideoPostCommandFromUser.UserId_That_Want_To_AddPost, addImageOrVideoPostCommandFromUser.Post_Privacy, addImageOrVideoPostCommandFromUser.ImageOrVideos));
                return New_Result(Result);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message, "From Add Post ");
                return BadRequest(ex.Message);
            }
        }
    }
}
