using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social_Media.API.Results_OF_API;
using Social_Media.Core.Features.Authentications.Commands.Models;
using Social_Media.Core.Features.Authentications.Queries.Models;

namespace Social_Media.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        public AuthenticationController(IMediator Mediator) : base(Mediator)
        {
        }

        [HttpPost("Login")]
        public IActionResult Login(SignInCommand signInCommandFromUser)
        {
            var Result = Mediator.Send(new SignInCommand(signInCommandFromUser.User_Email, signInCommandFromUser.User_Password)).Result;
            return New_Result(Result);
        }
        [HttpPost("Token/Refresh/Token")]
        public IActionResult RefreshToken([FromForm] RefreshTokenCommand refreshTokenCommandFromUser)
        {
            var Result = Mediator.Send(new RefreshTokenCommand(refreshTokenCommandFromUser.UserAccessToken, refreshTokenCommandFromUser.UserRefreshToken)).Result;
            return New_Result(Result);
        }
        [HttpPost("Email/ResendEmailConfirmation")]
        public IActionResult ResendEmailConfirmationLink(string Email)
        {
            var Result = Mediator.Send(new ResendConfirmationEmailCommand(Email)).Result;
            return New_Result(Result);
        }
        [HttpPost("ResetPasswordByEmail/{UserEmail}")]
        public IActionResult ResetPasswordByEmail(string UserEmail)
        {
            var Result = Mediator.Send(new SendResetPasswordByEmailQuery(UserEmail)).Result;
            return New_Result(Result);
        }
        [HttpPost("ResetPassword/ByPhoneNumber/{UserPhoneNumber}")]
        public IActionResult ResetPasswordByPhoneNumber(string UserPhoneNumber)
        {
            var Result = Mediator.Send(new SendResetPasswordByPhoneNumberQuery(UserPhoneNumber)).Result;
            return New_Result(Result);
        }
        [HttpGet("ConfirmEmail")]
        public IActionResult ConfirmEmail([FromQuery] string EmailToken, [FromQuery] string UserId)
        {
            var Result = Mediator.Send(new ConfirmEmailQuery(UserId, EmailToken)).Result;
            return New_Result(Result);
        }
    }
}
