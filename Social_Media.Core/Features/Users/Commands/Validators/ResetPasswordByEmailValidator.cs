using FluentValidation;
using Social_Media.Core.Features.Users.Commands.Models;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class ResetPasswordByEmailValidator : AbstractValidator<ResetPasswordByEmailCommand>
    {
        public ResetPasswordByEmailValidator()
        {
            ValidateResetPasswordByEmail();
        }

        public void ValidateResetPasswordByEmail()
        {
            RuleFor(R => R.Email)
                .EmailAddress().WithMessage("Invalid Email Format")
                .NotEmpty().WithMessage("Email Is Not Empty")
                .NotNull().WithMessage("Email Is Not Null");

            RuleFor(R => R.NewPassword)
                .NotEmpty().WithMessage("Password Is Not Empty")
                .NotNull().WithMessage("Password Is Not Null");
            RuleFor(R => R.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm Password Is Not Empty")
                .NotNull().WithMessage("Confirm Password Is Not Null")
                .Equal(R => R.NewPassword);
            RuleFor(R => R.CodeToResetPassword)
                .NotEmpty().WithMessage("CodeToResetPassword Is Not Empty")
                .NotNull().WithMessage("CodeToResetPassword Is Not Null");

        }
    }
}
