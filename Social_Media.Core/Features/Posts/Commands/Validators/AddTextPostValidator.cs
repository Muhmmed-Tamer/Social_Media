using FluentValidation;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    public class AddTextPostValidator : AbstractValidator<AddTextPostCommand>
    {
        private readonly IUserServices UserServices;
        public AddTextPostValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateAddTextPost();
        }
        public void ValidateAddTextPost()
        {
            RuleFor(TP => TP.Post_Content)
                .NotNull().WithMessage("Post Content Not Null")
                .NotEmpty().WithMessage("Post Content Not Empty");

            RuleFor(TP => TP.UserId_That_Want_To_AddPost)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User Is Not Found");
        }
    }
}
