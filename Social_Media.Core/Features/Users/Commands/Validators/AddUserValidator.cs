using FluentValidation;
using Social_Media.Core.Features.Users.Commands.Models;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            ValidateAddUser();
        }

        public void ValidateAddUser()
        {
            RuleFor(U => U.Password)
                .NotNull().WithMessage("Password Is Not Null")
                .NotEmpty().WithMessage("Password Is Not Empty");

            RuleFor(U => U.ConfirmPassword)
               .NotNull().WithMessage("ConfirmPassword Is Not Null")
               .NotEmpty().WithMessage("ConfirmPassword Is Not Empty")
               .Equal(U => U.Password);

            RuleFor(U => U.User_Email)
               .NotNull().WithMessage("Email Is Not Null")
               .NotEmpty().WithMessage("Email Is Not Empty")
               .EmailAddress().WithMessage("Invalid Email Address");
            RuleFor(U => U.User_FirstName_In_Arabic)
               .NotNull().WithMessage("User FirstName In Arabic Is Not Null")
               .NotEmpty().WithMessage("User FirstName In Arabic Is Not Empty");

            RuleFor(U => U.User_FirstName_In_English)
            .NotNull().WithMessage("User FirstName In English Is Not Null")
            .NotEmpty().WithMessage("User FirstName In English Is Not Empty");

            RuleFor(U => U.User_BirthDate)
                .NotNull().WithMessage("User BirthDate Is Not Null")
                .NotEmpty().WithMessage("User BirthDate Is Not Empty");
        }
    }
}
