using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Core.Features.Posts.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PostController : AppControllerBase
    {
        public PostController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("Add_TextPost")]
        public async Task<IActionResult> Add([FromForm] AddTextPostCommand addTextPostCommandFromUser)
        {
            var Result = await Mediator.Send(new AddTextPostCommand(addTextPostCommandFromUser.Post_Content, addTextPostCommandFromUser.UserId_That_Want_To_AddPost, addTextPostCommandFromUser.Post_Privacy));
            return New_Result(Result);
        }
        [HttpPost("Add/ImageOrVideoPost")]
        public async Task<IActionResult> Add([FromForm] AddImageOrVideoPostCommand addImageOrVideoPostCommandFromUser)
        {
            var Result = await Mediator.Send(new AddImageOrVideoPostCommand(addImageOrVideoPostCommandFromUser.Post_Title, addImageOrVideoPostCommandFromUser.UserId_That_Want_To_AddPost, addImageOrVideoPostCommandFromUser.Post_Privacy, addImageOrVideoPostCommandFromUser.ImageOrVideos));
            return New_Result(Result);
        }
        [HttpGet("GetPostsOFUser/{UserId:guid}")]
        public IActionResult GetPostsOFUser(string UserId)
        {
            var Result = Mediator.Send(new GetPostsOFUserQuery(UserId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("Delete/Post")]
        public async Task<IActionResult> DeletePost([FromBody] DeletePostCommand deleteTextPostCommand)
        {
            var Result = Mediator.Send(new DeletePostCommand(deleteTextPostCommand.Id, deleteTextPostCommand.UserIdWhoWantToDelete)).Result;
            return New_Result(Result);

        }
        [HttpPut("Update/TextPost")]
        public async Task<IActionResult> UpdateTextPost([FromBody] UpdatePostCommand updatePostFromUser)
        {
            var Result = Mediator.Send(new UpdatePostCommand(updatePostFromUser.PostId, updatePostFromUser.New_Content, updatePostFromUser.UserId)).Result;

            return New_Result(Result);
        }
        [HttpPut("Update/MediaPost")]
        public async Task<IActionResult> UpdateMediaPost([FromForm] UpdateMediaPostCommand UpdateMediaPostCommandFromUser)
        {

            var Result = Mediator.Send(new UpdateMediaPostCommand(UpdateMediaPostCommandFromUser.PostId, UpdateMediaPostCommandFromUser.Caption, UpdateMediaPostCommandFromUser.UserId, UpdateMediaPostCommandFromUser.Media)).Result;
            return New_Result(Result);
        }


    }
}
