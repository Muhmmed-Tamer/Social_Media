using FluentValidation;
using Social_Media.Core.Features.Authentications.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Authentications.Queries.Validators
{
    public class SendResetPasswordByEmailValidator : AbstractValidator<SendResetPasswordByEmailQuery>
    {
        private readonly IUserServices UserServices;
        public SendResetPasswordByEmailValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateIFuserIsExistOrNot();
        }

        public void ValidateIFuserIsExistOrNot()
        {
            RuleFor(U => U.User_Email)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByEmailAsync(Key) is not null)
                .WithMessage("User Is Not Found");
        }
    }
}
