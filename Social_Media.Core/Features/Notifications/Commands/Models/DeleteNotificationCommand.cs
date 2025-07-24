using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Notifications.Commands.Models
{
    public class DeleteNotificationCommand : IRequest<Response<string>>
    {

        public int NotificationId { get; set; }
        public string UserIdThatWantToDeleteNotification { get; set; }
        public DeleteNotificationCommand(int NotificationId, string UserIdThatWantToDeleteNotification)
        {
            this.NotificationId = NotificationId;
            this.UserIdThatWantToDeleteNotification = UserIdThatWantToDeleteNotification;
        }

    }
}
