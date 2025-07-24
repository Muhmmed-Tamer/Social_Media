using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Features.Notifications.Commands.Handlers
{
    public class NotificationHandler : ResponseHandler, IRequestHandler<DeleteNotificationCommand, Response<string>>

    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;

        public NotificationHandler(ILogger logger, IUnitOFWork unitOFWork)
        {
            Logger = logger;
            UnitOFWork = unitOFWork;
        }

        public async Task<Response<string>> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.NotificationUnitOFWork.NotificationServices.BeginTransaction())
            {
                try
                {
                    Notification Notification_ThatWantToDelete = await UnitOFWork.NotificationUnitOFWork.NotificationServices.GetByIdAsync(command.NotificationId);
                    if (Notification_ThatWantToDelete.UserIdWhoReceivedTheNotification != command.UserIdThatWantToDeleteNotification)
                    {
                        return BadRequest<string>("The User Not Have This Notification");
                    }
                    Notification_ThatWantToDelete.IsDeleted = true;
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.UpdateAsync(Notification_ThatWantToDelete);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();
                    await UpdateNotificationType(Notification_ThatWantToDelete);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.CommitTransaction(Transaction);
                    return OK("Notification Is Deleted Successfully");
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    Logger.Error(ex.Message);
                    return BadRequest<string>(ex.Message);

                }
            }
        }

        private async Task UpdateNotificationType(Notification notificationThatWantToDelete)
        {
            try
            {
                switch (notificationThatWantToDelete.NotificationType)
                {
                    case NotificationType.AddPost:
                        var postNotification = await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (postNotification is not null)
                        {
                            postNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.UpdateAsync(postNotification);
                            await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.SaveChangesAsync();
                        }
                        break;

                    case NotificationType.SendMessage:
                        var messageNotification = await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (messageNotification is not null)
                        {
                            messageNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.UpdateAsync(messageNotification);
                            await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.SaveChangesAsync();
                        }
                        break;

                    case NotificationType.AddComment:
                        var commentNotification = await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (commentNotification is not null)
                        {
                            commentNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.UpdateAsync(commentNotification);
                            await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.SaveChangesAsync();
                        }
                        break;

                    case NotificationType.SendNewFriendRequest:
                        var friendRequestNotification = await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (friendRequestNotification is not null)
                        {
                            friendRequestNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.UpdateAsync(friendRequestNotification);
                            await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.SaveChangesAsync();
                        }
                        break;

                    case NotificationType.ConfirmFriendRequest:
                        var confirmFriendRequestNotification = await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (confirmFriendRequestNotification is not null)
                        {
                            confirmFriendRequestNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.UpdateAsync(confirmFriendRequestNotification);
                            await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.SaveChangesAsync();
                        }
                        break;

                    case NotificationType.InteractionWithPost:
                        var interactionPostNotification = await UnitOFWork.NotificationUnitOFWork.InteractionNotificationByPostServices.GetByNotificationId(notificationThatWantToDelete.Id);
                        if (interactionPostNotification is not null)
                        {
                            interactionPostNotification.IsDeleted = true;
                            await UnitOFWork.NotificationUnitOFWork.InteractionNotificationByPostServices.UpdateAsync(interactionPostNotification);
                            await UnitOFWork.NotificationUnitOFWork.InteractionNotificationByPostServices.SaveChangesAsync();
                        }
                        break;

                    default:
                        break;
                }

            }

            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

    }
}
