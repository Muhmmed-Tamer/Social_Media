using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Authentications.Queries.Models
{
    public class SendResetPasswordByPhoneNumberQuery : IRequest<Response<string>>
    {
        public string PhoneNumber { get; set; }
        public SendResetPasswordByPhoneNumberQuery()
        {
        }
        public SendResetPasswordByPhoneNumberQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
