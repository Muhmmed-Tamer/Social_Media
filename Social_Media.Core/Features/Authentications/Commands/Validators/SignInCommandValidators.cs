using FluentValidation;
using Social_Media.Core.Features.Authentications.Commands.Models;

namespace Social_Media.Core.Features.Authentications.Commands.Validators
{
    public class SignInCommandValidators : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidators()
        {
            ValidateUserEmail();
            ValidateUserPassword();
        }

        public void ValidateUserEmail()
        {
            RuleFor(S => S.User_Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid Email Format.");
        }
        public void ValidateUserPassword()
        {
            RuleFor(S => S.User_Password)
                .NotNull().WithMessage("Password cannot be null.")
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
        }
    }
}
