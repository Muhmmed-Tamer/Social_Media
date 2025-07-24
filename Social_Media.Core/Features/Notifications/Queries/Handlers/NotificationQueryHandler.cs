using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Queries.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Core.Response_Structure.Pagination;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Features.Notifications.Queries.Handlers
{
    public class NotificationQueryHandler : ResponseHandler, IRequestHandler<GetNotificationOFUserQuery, Response<List<GetNotificationOFUser>>>,
        IRequestHandler<GetNotificationOFUserAsQueryableQuery, Response<Paginated<GetNotificationOFUser>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public NotificationQueryHandler(IUnitOFWork unitOFWork, ILogger logger)
        {
            UnitOFWork = unitOFWork;
            Logger = logger;
        }

        public async Task<Response<List<GetNotificationOFUser>>> Handle(GetNotificationOFUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Notification> AllNotificationOFUser = await UnitOFWork.NotificationUnitOFWork.NotificationServices.GetAllNotificationOFUser(request.UserId);
                List<GetNotificationOFUser> Mapped_Notifications = UnitOFWork.Mapper.Map<List<GetNotificationOFUser>>(AllNotificationOFUser);
                return OK(Mapped_Notifications);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while handling GetNotificationOFUserQuery for UserId: {UserId}", request.UserId);
                return BadRequest<List<GetNotificationOFUser>>(ex.Message);
            }
        }

        public async Task<Response<Paginated<GetNotificationOFUser>>> Handle(GetNotificationOFUserAsQueryableQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<Notification> AllNotificationOFUser = await UnitOFWork.NotificationUnitOFWork.NotificationServices.GetAllNotificationOFUserAsQueryable(request.UserId);
                var Mapped_AllNotificationOFUser = UnitOFWork.Mapper.ProjectTo<GetNotificationOFUser>(AllNotificationOFUser);
                Paginated<GetNotificationOFUser> AllNotificationOFUserAsPaginated = await Mapped_AllNotificationOFUser.ToPaginatedAsync(request.PageNumber, request.PageSize);
                return OK(AllNotificationOFUserAsPaginated);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return BadRequest<Paginated<GetNotificationOFUser>>(ex.Message);
            }
        }
    }
}
