using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Authentications.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public ConfirmEmailQuery()
        {

        }
        public ConfirmEmailQuery(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }

    }
}
