using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Queries.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Features.Notifications.Queries.Handlers
{
    public class NotificationQueryHandler : ResponseHandler, IRequestHandler<GetNotificationOFUserQuery, Response<List<GetNotificationOFUser>>>
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
                List<Notification> AllNotificationOFUser = await UnitOFWork.NotificationServices.GetAllNotificationOFUser(request.UserId);
                List<GetNotificationOFUser> Mapped_Notifications = UnitOFWork.Mapper.Map<List<GetNotificationOFUser>>(AllNotificationOFUser);
                return OK(Mapped_Notifications);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while handling GetNotificationOFUserQuery for UserId: {UserId}", request.UserId);
                return BadRequest<List<GetNotificationOFUser>>(ex.Message);
            }
        }
    }
}
