using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Commands.Models;
using Social_Media.Core.Implementation_UnitOFWork;
using Social_Media.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Notifications.Commands.Validators
{
    public class DeleteNotificationValidator : AbstractValidator<DeleteNotificationCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public DeleteNotificationValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            RuleFor(x => x.Id)
            .MustAsync(NotificationExistsAsync)
            .WithMessage("The notification you're trying to delete does not exist.");
        }
        private async Task<bool> NotificationExistsAsync(DeleteNotificationCommand command, int id, ValidationContext<DeleteNotificationCommand> context, CancellationToken cancellationToken)
        {
            return command.Type switch
            {
                NotificationType.AddPost =>
                    await unitOFWork.NotificationUnitOFWork.PostNotificationServices.GetByIdAsync(id) is not null,
                NotificationType.SendMessage =>
                    await unitOFWork.NotificationUnitOFWork.MessageNotificationServices.GetByIdAsync(id) is not null,
                NotificationType.InteractionWithPost =>
                    await unitOFWork.InteractionUnitOFWork.InteractionNotificationByPostServices.GetByIdAsync(id) is not null,
                NotificationType.InteractionWithStory =>
                    await unitOFWork.InteractionUnitOFWork.InteractionNotificationByStoryServices.GetByIdAsync(id) is not null,
                NotificationType.InteractionWithComment =>
                    await unitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetByIdAsync(id) is not null,
                NotificationType.AddComment =>
                    await unitOFWork.NotificationUnitOFWork.CommentNotificationService.GetByIdAsync(id) is not null,

                NotificationType.SendNewFriendRequest =>
                    await unitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.GetByIdAsync(id) is not null,

                NotificationType.ConfirmFriendRequest =>
                    await unitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.GetByIdAsync(id) is not null,

                _ => false
            };
        }



    }
}
