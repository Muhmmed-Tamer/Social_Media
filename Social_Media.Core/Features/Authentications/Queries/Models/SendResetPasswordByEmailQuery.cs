using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Authentications.Queries.Models
{
    public class SendResetPasswordByEmailQuery : IRequest<Response<string>>
    {
        public string User_Email { get; set; }
        public SendResetPasswordByEmailQuery()
        {
        }
        public SendResetPasswordByEmailQuery(string user_Email)
        {
            User_Email = user_Email;
        }
    }
}
