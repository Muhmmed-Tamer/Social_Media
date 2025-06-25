using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Authentications.Commands.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string User_Email { get; set; }
        public string User_Password { get; set; }

        public SignInCommand()
        {

        }
        public SignInCommand(string User_Email, string User_Password)
        {
            this.User_Email = User_Email;
            this.User_Password = User_Password;
        }
    }
}
