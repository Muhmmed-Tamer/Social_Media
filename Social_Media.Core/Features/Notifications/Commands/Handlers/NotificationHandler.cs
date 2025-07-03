using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Core.Features.Comments.Commands.Validators;
using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Core.Features.Notifications.Commands.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;

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

        private async Task<object?> GetObject(DeleteNotificationCommand command)
        {
            object? result = command.Type switch
            {
                NotificationType.AddPost =>
                    await UnitOFWork.NotificationUnitOFWork.PostNotificationServices.GetByIdAsync(command.Id),

                NotificationType.SendMessage =>
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.GetByIdAsync(command.Id),

                NotificationType.InteractionWithPost =>
                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByPostServices.GetByIdAsync(command.Id),

                NotificationType.InteractionWithStory =>
                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByStoryServices.GetByIdAsync(command.Id),

                NotificationType.InteractionWithComment =>
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetByIdAsync(command.Id),

                NotificationType.AddComment =>
                    await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.GetByIdAsync(command.Id),

                NotificationType.SendNewFriendRequest =>
                    await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.GetByIdAsync(command.Id),

                NotificationType.ConfirmFriendRequest =>
                    await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.GetByIdAsync(command.Id),

                _ => null
            };

            return result;

           
        }





        public async Task<Response<string>> Handle(DeleteNotificationCommand command, CancellationToken cancellationToken)
        {
            using(var Transaction = await UnitOFWork.NotificationUnitOFWork.NotificationServices.BeginTransaction())
            {
                try
                {

                    dynamic Notification = await this.GetObject(command);
                   
                    Notification.IsDelete=true;

                    await UnitOFWork.ContextData.SaveChangesAsync();
                    await Transaction.CommitAsync();

                    return OK("Comment Is Deleted Successfully");


                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    Logger.Error(ex.Message);
                    return BadRequest<string>(ex.Message);

                }


            }
        }
    }
}
