using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Notifications.Commands.Models;

namespace Social_Media.Core.Features.Notifications.Commands.Validators
{
    public class DeleteNotificationValidator : AbstractValidator<DeleteNotificationCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public DeleteNotificationValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateNotificationIdAndUserIdNotNullOrEmpty();
            ValidateIdfUserIsFoundOrNot();
        }
        public void ValidateNotificationIdAndUserIdNotNullOrEmpty()
        {
            RuleFor(N => N.NotificationId)
                .NotEmpty().WithMessage("NotificationId Should Not Null")
                .NotNull().WithMessage("NotificationId Should Not Empty");

            RuleFor(N => N.UserIdThatWantToDeleteNotification)
                .NotEmpty().WithMessage("UserIdThatWantToDeleteNotification Should Not Null")
                .NotNull().WithMessage("UserIdThatWantToDeleteNotification Should Not Empty");
        }
        public void ValidateIdfUserIsFoundOrNot()
        {
            RuleFor(U => U.UserIdThatWantToDeleteNotification)
                .MustAsync(async (Key, Cancelation) => await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key) is not null)
                .WithMessage("User Is Not Found");
        }

    }
}
