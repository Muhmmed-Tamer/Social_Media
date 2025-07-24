using FluentValidation;
using Social_Media.Core.Features.Users.Queries.Models;

namespace Social_Media.Core.Features.Users.Queries.Validators
{
    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            ValidateGetUserByEmailQuery();
        }
        public void ValidateGetUserByEmailQuery()
        {
            RuleFor(U => U.User_Email)
                .NotEmpty().WithMessage("Email is Required.")
                .NotNull().WithMessage("Email Must Be Not Null")
                .EmailAddress().WithMessage("Invalid Email Format.");
        }
    }
}
