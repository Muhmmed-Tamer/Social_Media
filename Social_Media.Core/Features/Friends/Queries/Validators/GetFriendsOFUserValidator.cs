using FluentValidation;
using Social_Media.Core.Features.Friends.Queries.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Friends.Queries.Validators
{
    public class GetFriendsOFUserValidator : AbstractValidator<GetFriendsOFUserQuery>
    {
        private readonly IUserServices UserServices;
        public GetFriendsOFUserValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
        }

        public void ValidateGetFriendsOFUserQuery()
        {
            RuleFor(U => U.UserId).NotEmpty().WithMessage("UserId is Required.");
            RuleFor(U => U.UserId).NotNull().WithMessage("UserId cannot be null.");

            RuleFor(U => U.UserId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false);
        }
    }
}
