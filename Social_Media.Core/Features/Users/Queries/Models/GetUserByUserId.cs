using MediatR;
using Social_Media.Core.Features.Users.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Users.Queries.Models
{
    public class GetUserByUserId : IRequest<Response<GetUser>>
    {
        public string UserId { get; set; }
        public GetUserByUserId()
        {

        }
        public GetUserByUserId(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
