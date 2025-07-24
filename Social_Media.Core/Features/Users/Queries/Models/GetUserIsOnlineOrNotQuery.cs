using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Users.Queries.Models
{
    public class GetUserIsOnlineOrNotQuery : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public GetUserIsOnlineOrNotQuery()
        {

        }
        public GetUserIsOnlineOrNotQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
