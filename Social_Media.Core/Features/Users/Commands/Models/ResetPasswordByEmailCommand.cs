using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class ResetPasswordByEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string CodeToResetPassword { get; set; }
        public ResetPasswordByEmailCommand()
        {

        }
        public ResetPasswordByEmailCommand(string Email, string NewPassword, string ConfirmNewPassword, string CodeToResetPassword)
        {
            this.Email = Email;
            this.NewPassword = NewPassword;
            this.ConfirmNewPassword = ConfirmNewPassword;
            this.CodeToResetPassword = CodeToResetPassword;
        }
    }
}
