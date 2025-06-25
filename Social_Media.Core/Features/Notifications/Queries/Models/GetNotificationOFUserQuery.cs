using MediatR;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Notifications.Queries.Models
{
    public class GetNotificationOFUserQuery : IRequest<Response<List<GetNotificationOFUser>>>
    {
        public string UserId { get; set; }

        public GetNotificationOFUserQuery()
        {

        }
        public GetNotificationOFUserQuery(string UserId)
        {
            this.UserId = UserId;
        }
    }
}
