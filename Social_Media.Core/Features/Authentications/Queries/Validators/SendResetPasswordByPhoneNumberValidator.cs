using FluentValidation;
using Social_Media.Core.Features.Authentications.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Authentications.Queries.Validators
{
    public class SendResetPasswordByPhoneNumberValidator : AbstractValidator<SendResetPasswordByPhoneNumberQuery>
    {
        private readonly IUserServices UserServices;
        public SendResetPasswordByPhoneNumberValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateUserIsFoundOrNot();
        }
        public void ValidateUserIsFoundOrNot()
        {
            RuleFor(U => U.PhoneNumber)
                .MustAsync(async (Key, CancelationToken) => await UserServices.GetUserByPhoneNumberAsync(Key) is not null)
                .WithMessage("User Is Not Found");
        }
    }
}
