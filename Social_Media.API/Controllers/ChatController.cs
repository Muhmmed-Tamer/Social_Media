using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Core.Features.Chats.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ChatController : AppControllerBase
    {
        public ChatController(IMediator Mediator) : base(Mediator)
        {
        }
        [HttpPost("Add/TextMessage")]
        public IActionResult AddTextMessage([FromForm] AddTextMessageCommand addTextMessageCommandFromUser)
        {
            var Result = Mediator.Send(new AddTextMessageCommand(addTextMessageCommandFromUser.SenderId, addTextMessageCommandFromUser.ReceiverId, addTextMessageCommandFromUser.Content)).Result;
            return New_Result(Result);
        }
        [HttpPost("AddAudioMessage")]
        public IActionResult AddAudioMessage([FromForm] AddAudioMessageCommand addAudioMessageCommandFromUser)
        {
            var Result = Mediator.Send(new AddAudioMessageCommand(addAudioMessageCommandFromUser.SenderId, addAudioMessageCommandFromUser.ReceiverId, addAudioMessageCommandFromUser.Audio)).Result;
            return New_Result(Result);
        }
        [HttpPost("Add/Media/Message")]
        public IActionResult AddMediaMessage([FromForm] AddMediaMessageCommand addMediaMessageCommandFromUser)
        {
            var Result = Mediator.Send(new AddMediaMessageCommand(addMediaMessageCommandFromUser.SenderId, addMediaMessageCommandFromUser.ReceiverId, addMediaMessageCommandFromUser.Medias)).Result;
            return New_Result(Result);
        }
        [HttpGet("GetMessages{SenderId:guid}/{ReceiverId:guid}")]
        public IActionResult GetChatBetweenTwoUsers(string SenderId, string ReceiverId)
        {
            var Result = Mediator.Send(new GetMessagesBetweenTwoUsersQuery(SenderId, ReceiverId)).Result;
            return New_Result(Result);
        }
        [HttpDelete("Message/Delete")]
        public IActionResult DeleteMessage([FromBody] DeleteMessageCommand deleteMessageCommand)
        {
            var Result = Mediator.Send(new DeleteMessageCommand(deleteMessageCommand.Id, deleteMessageCommand.SenderId, deleteMessageCommand.ReceiverId, deleteMessageCommand.UserWhoWantToDelete)).Result;
            return New_Result(Result);
        }
        [HttpPatch("Edit/TextMessage")]
        public IActionResult UpdateMessage(UpdateMessageCommand updateMessageCommandFromUser)
        {
            var Result = Mediator.Send(new UpdateMessageCommand(updateMessageCommandFromUser.MessageId, updateMessageCommandFromUser.New_Content, updateMessageCommandFromUser.UserIdWhoWantToUpdate)).Result;
            return New_Result(Result);
        }
    }
}
