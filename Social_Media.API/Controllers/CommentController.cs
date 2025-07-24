using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Core.Features.Comments.Queires.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CommentController : AppControllerBase
    {
        public CommentController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("AddComment")]
        public IActionResult AddCommentToImageOrVideoPost(AddCommentToPostCommand addCommentToPostCommandFromUser)
        {
            var Result = Mediator.Send(new AddCommentToPostCommand(addCommentToPostCommandFromUser.PostId, addCommentToPostCommandFromUser.Comment_Content, addCommentToPostCommandFromUser.UserId)).Result;
            return New_Result(Result);
        }
        [HttpPost("AddReplyOFComment")]
        public IActionResult AddReplyOFComment([FromForm] AddReplyOFCommentCommand addReplyOFCommentCommandFromUser)
        {
            var Result = Mediator.Send(new AddReplyOFCommentCommand(addReplyOFCommentCommandFromUser.CommentId, addReplyOFCommentCommandFromUser.Content,
                addReplyOFCommentCommandFromUser.UserIdThatWriteAComment, addReplyOFCommentCommandFromUser.UserIdThatWriteAReplyOFComment)).Result;
            return New_Result(Result);
        }
        [HttpGet("GetCommentsByPostId{PostId:int}")]
        public IActionResult GetCommentsByPostId(int PostId)
        {
            var Result = Mediator.Send(new GetCommentsOFPostQuery(PostId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("DeleteComment")]
        public IActionResult DeleteCommentByPostId([FromBody] DeleteCommentCommand deleteCommentCommand)
        {
            var Result = Mediator.Send(new DeleteCommentCommand(deleteCommentCommand.Id, deleteCommentCommand.PostId, deleteCommentCommand.UserIdHowWantToDelete)).Result;

            return New_Result(Result);

        }
        [HttpPatch("Comment/Edit")]
        public IActionResult EditComment([FromBody] UpdateCommentCommand editCommentCommand)
        {
            var Result = Mediator.Send(new UpdateCommentCommand(editCommentCommand.CommentId, editCommentCommand.UserIdWhoWantToUpdate, editCommentCommand.New_Content)).Result;
            return New_Result(Result);
        }

    }
}
