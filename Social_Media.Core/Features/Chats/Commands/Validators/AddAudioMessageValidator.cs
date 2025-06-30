using FluentValidation;
using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Chats.Commands.Validators
{
    public class AddAudioMessageValidator : AbstractValidator<AddAudioMessageCommand>
    {
        private readonly IUserServices UserServices;
        public AddAudioMessageValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateSenderUserIdNotEqualReceiverUserId();
            ValidateSenderUserAndReceiverUserIsFound();
            ValidateTextMessage();
        }

        public void ValidateTextMessage()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Sender ID cannot be empty.")
                .NotNull().WithMessage("Sender ID cannot be Null.");

            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Receiver ID cannot be empty.")
                .NotNull().WithMessage("Receiver ID cannot be Null.");

            RuleFor(x => x.Audio.FileName)
                .NotEmpty().WithMessage("Audio cannot be empty.")
                .NotNull().WithMessage("Audio cannot be Null.")
                .MaximumLength(500).WithMessage("Content cannot exceed 500 characters.");
        }

        public void ValidateSenderUserIdNotEqualReceiverUserId()
        {
            RuleFor(M => M)
                .Must(M => M.SenderId != M.ReceiverId)
                .WithMessage("Sender ID cannot be the same as Receiver ID.");
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
