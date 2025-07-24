using MediatR;
using Social_Media.Core.Features.Chats.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Chats.Queries.Models
{
    public class GetMessagesBetweenTwoUsersQuery : IRequest<Response<List<GetMessagesBetweenTwoUsers>>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public GetMessagesBetweenTwoUsersQuery() { }
        public GetMessagesBetweenTwoUsersQuery(string SenderId, string ReceiverId)
        {
            this.SenderId = SenderId;
            this.ReceiverId = ReceiverId;
        }
    }
}
