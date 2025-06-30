using MediatR;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Core.Response_Structure.Pagination;

namespace Social_Media.Core.Features.Notifications.Queries.Models
{
    public class GetNotificationOFUserAsQueryableQuery : IRequest<Response<Paginated<GetNotificationOFUser>>>
    {
        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public GetNotificationOFUserAsQueryableQuery()
        {

        }

        public GetNotificationOFUserAsQueryableQuery(string UserId, int PageSize, int PageNumber)
        {
            this.UserId = UserId;
            this.PageSize = PageSize;
            this.PageNumber = PageNumber;
        }
    }
}
