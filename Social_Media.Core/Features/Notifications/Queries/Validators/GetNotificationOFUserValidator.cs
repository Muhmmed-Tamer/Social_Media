using FluentValidation;
using Social_Media.Core.Features.Notifications.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Notifications.Queries.Validators
{
    public class GetNotificationOFUserValidator : AbstractValidator<GetNotificationOFUserQuery>
    {
        private readonly IUserServices UserServices;
        public GetNotificationOFUserValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateGetNotificationOFUser();
        }

        public void ValidateGetNotificationOFUser()
        {
            RuleFor(U => U.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .NotNull().WithMessage("UserId cannot be null.");

            RuleFor(U => U.UserId)
                .MustAsync(async (Key, CancellationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User does not exist.");


        }
    }
}
