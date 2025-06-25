using FluentValidation;
using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Friend_Request_Notifications.Commands.Validators
{
    public class AddFriendRequestValidator : AbstractValidator<AddFriendRequestCommand>
    {
        private readonly IUserServices UserServices;
        public AddFriendRequestValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateSenderUserIsFound();
            ValidateReceiveUserIsFound();
            ValidateSenderAndReceiveUserIsNotSame();
        }
        public void ValidateSenderUserIsFound()
        {
            RuleFor(U => U.UserId_ThatSentFriendRequest)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("Sender User Is Not Found");
        }
        public void ValidateReceiveUserIsFound()
        {
            RuleFor(U => U.UserId_ThatReceiveFriendRequest)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("Sender User Is Not Found");
        }
        public void ValidateSenderAndReceiveUserIsNotSame()
        {
            RuleFor(U => U.UserId_ThatReceiveFriendRequest)
                .NotEqual(U => U.UserId_ThatSentFriendRequest).WithMessage("Sender User And Receive User Is Same");
        }
    }
}
