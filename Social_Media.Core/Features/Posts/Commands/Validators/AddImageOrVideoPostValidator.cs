using FluentValidation;
using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    public class AddImageOrVideoPostValidator : AbstractValidator<AddImageOrVideoPostCommand>
    {
        private readonly IUserServices UserServices;
        public AddImageOrVideoPostValidator(IUserServices UserServices)
        {
            this.UserServices = UserServices;
            ValidateAddImageOrVideoPost();
        }
        public void ValidateAddImageOrVideoPost()
        {
            RuleFor(TP => TP.UserId_That_Want_To_AddPost)
               .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
               .WithMessage("User Is Not Found");
            RuleFor(TP => TP.ImageOrVideos)
                .NotNull().WithMessage("Image Or Video Is Not Null")
                .NotEmpty().WithMessage("Image Or Video Is Not Empty");

        }
    }
}
