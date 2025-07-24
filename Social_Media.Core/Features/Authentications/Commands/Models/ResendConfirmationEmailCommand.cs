using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Authentications.Commands.Models
{
    public class ResendConfirmationEmailCommand : IRequest<Response<string>>
    {
        public string User_Email { get; set; }
        public ResendConfirmationEmailCommand()
        {

        }
        public ResendConfirmationEmailCommand(string User_Email)
        {
            this.User_Email = User_Email;
        }
    }
}
