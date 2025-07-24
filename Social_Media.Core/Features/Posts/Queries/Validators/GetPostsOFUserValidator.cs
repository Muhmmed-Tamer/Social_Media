using FluentValidation;
using Social_Media.Core.Features.Posts.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Posts.Queries.Validators
{
    public class GetPostsOFUserValidator : AbstractValidator<GetPostsOFUserQuery>
    {
        private readonly IUserServices UserServices;
        public GetPostsOFUserValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
        }
        public void ValidateUserId()
        {
            RuleFor(U => U.UserId)
                .NotEmpty().WithMessage("User ID cannot be empty.")
                .NotNull().WithMessage("User ID cannot be null.");

            RuleFor(U => U.UserId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("The User Is Not Found");
        }
    }
}
