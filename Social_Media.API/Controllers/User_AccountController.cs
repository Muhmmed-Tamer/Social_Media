using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Users.Commands.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_AccountController : AppControllerBase
    {
        private readonly ILogger<User_AccountController> Logger;
        public User_AccountController(IMediator Mediator, ILogger<User_AccountController> logger) : base(Mediator)
        {
            Logger = logger;
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromForm] AddUserCommand addUserCommandFromUser)
        {
            try
            {
                var Result = Mediator.Send(new AddUserCommand(addUserCommandFromUser.User_FirstName_In_English, addUserCommandFromUser.User_LastName_In_English,
                    addUserCommandFromUser.User_FirstName_In_Arabic, addUserCommandFromUser.User_LastName_In_Arabic, addUserCommandFromUser.User_Email,
                    addUserCommandFromUser.User_Mobile, addUserCommandFromUser.User_Gender, addUserCommandFromUser.User_BirthDate, addUserCommandFromUser.Password, addUserCommandFromUser.ConfirmPassword, addUserCommandFromUser.User_Picture)
                ).Result;
                return New_Result(Result);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
