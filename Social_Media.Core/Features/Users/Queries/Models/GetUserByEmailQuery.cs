using MediatR;
using Social_Media.Core.Features.Users.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Users.Queries.Models
{
    public class GetUserByEmailQuery : IRequest<Response<GetUser>>
    {
        public string User_Email { get; set; }
        public GetUserByEmailQuery(string userEmail)
        {
            User_Email = userEmail;
        }
        public GetUserByEmailQuery()
        {

        }
    }
}
