using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Users.Commands.Models;
using Social_Media.Core.Features.Users.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_AccountController : AppControllerBase
    {
        public User_AccountController(IMediator Mediator) : base(Mediator)
        {
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromForm] AddUserCommand addUserCommandFromUser)
        {
            var Result = Mediator.Send(new AddUserCommand(addUserCommandFromUser.User_FirstName_In_English, addUserCommandFromUser.User_LastName_In_English,
                addUserCommandFromUser.User_FirstName_In_Arabic, addUserCommandFromUser.User_LastName_In_Arabic, addUserCommandFromUser.User_Email,
                addUserCommandFromUser.User_Mobile, addUserCommandFromUser.User_Gender, addUserCommandFromUser.User_BirthDate, addUserCommandFromUser.Password, addUserCommandFromUser.ConfirmPassword, addUserCommandFromUser.User_Picture)
            ).Result;
            return New_Result(Result);
        }
        [HttpPost("ResetPassword/ByCodeFromEmail")]
        public IActionResult ResetPasswordByCodeFromEmail(ResetPasswordByEmailCommand resetPasswordByEmailCommand)
        {
            var Result = Mediator.Send(new ResetPasswordByEmailCommand(resetPasswordByEmailCommand.Email, resetPasswordByEmailCommand.NewPassword, resetPasswordByEmailCommand.ConfirmNewPassword, resetPasswordByEmailCommand.CodeToResetPassword)).Result;
            return New_Result(Result);
        }
        [HttpGet("GetUser/{Email}")]

        public IActionResult GetUserByEmail(string Email)
        {
            var Result = Mediator.Send(new GetUserByEmailQuery(Email)).Result;
            return New_Result(Result);
        }
        [HttpGet("Get/User/{UserId:guid}")]
        public IActionResult GetUserById(string UserId)
        {
            var Result = Mediator.Send(new GetUserByUserId(UserId)).Result;
            return New_Result(Result);
        }
    }
}
