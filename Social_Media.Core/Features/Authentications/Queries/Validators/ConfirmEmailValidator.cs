using FluentValidation;
using Social_Media.Core.Features.Authentications.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Authentications.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        private readonly IUserServices UserServices;
        public ConfirmEmailValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateIfUserIsFoundOrNot();
        }

        public void ValidateIfUserIsFoundOrNot()
        {
            RuleFor(U => U.UserId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null)
                .WithMessage("User Is Not Found");
        }
    }
}
