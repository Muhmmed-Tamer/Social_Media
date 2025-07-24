using FluentValidation;
using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Validators
{
    public class AddInteractionWithPostValidator : AbstractValidator<AddInteractionWithPostCommand>
    {
        private readonly IUserServices UserServices;
        private readonly IPostServices PostServices;
        public AddInteractionWithPostValidator(IUserServices UserServices, IPostServices PostServices)
        {
            this.UserServices = UserServices;
            this.PostServices = PostServices;
            ValidateUserIsFound();
            ValidatePostIsFound();
            InteractionWithPostIsNotNllOrEmpty();
        }
        public void ValidateUserIsFound()
        {
            RuleFor(U => U.UserIdThatInteractWithPost)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User Is Not Found");
        }
        public void ValidatePostIsFound()
        {
            RuleFor(P => P.PostIdThatInteractWith)
                .MustAsync(async (Key, CancelationToken) => await PostServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Post Is Not Found");
        }
        public void InteractionWithPostIsNotNllOrEmpty()
        {
            RuleFor(I => I.InteractionBy)
                .NotNull().WithMessage("InteractionBy Not Null")
                .NotEmpty().WithMessage("InteractionBy Not Empty");
        }
    }
}
