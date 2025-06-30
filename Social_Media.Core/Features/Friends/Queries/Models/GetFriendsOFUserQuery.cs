using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Friends.Queries.Models
{
    public class GetFriendsOFUserQuery : IRequest<Response<List<string>>>
    {
        public string UserId { get; set; }

        public GetFriendsOFUserQuery()
        {

        }
        public GetFriendsOFUserQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
