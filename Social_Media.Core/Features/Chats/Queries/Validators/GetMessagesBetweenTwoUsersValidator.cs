using FluentValidation;
using Social_Media.Core.Features.Chats.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Chats.Queries.Validators
{
    public class GetMessagesBetweenTwoUsersValidator : AbstractValidator<GetMessagesBetweenTwoUsersQuery>
    {
        private readonly IUserServices UserServices;
        public GetMessagesBetweenTwoUsersValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateSenderUserAndReceiverUserIsNotNullOrEmpty();
            ValidateSenderUserAndReceiverUserAreNotEqual();
            ValidateSenderUserAndReceiverUserIsFound();
        }
        public void ValidateSenderUserAndReceiverUserIsNotNullOrEmpty()
        {
            RuleFor(M => M.SenderId)
                .NotEmpty().WithMessage("SenderId cannot be empty.")
                .NotNull().WithMessage("SenderId cannot be null.");
            RuleFor(M => M.ReceiverId)
                .NotEmpty().WithMessage("ReceiverId cannot be empty.")
                .NotNull().WithMessage("ReceiverId cannot be null.");
        }
        public void ValidateSenderUserAndReceiverUserAreNotEqual()
        {
            RuleFor(M => M)
                .Must(M => M.SenderId != M.ReceiverId)
                .WithMessage("SenderId and ReceiverId cannot be the same.");
        }
        public void ValidateSenderUserAndReceiverUserIsFound()
        {
            RuleFor(M => M.SenderId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("Sender User Is Not Found");

            RuleFor(M => M.ReceiverId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("Receiver User Is Not Found");
        }
    }
}
