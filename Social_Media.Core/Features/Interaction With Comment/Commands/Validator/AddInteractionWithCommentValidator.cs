using FluentValidation;
using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using Social_Media.Services.AbstractsServices.CommentServices;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Validator
{
    public class AddInteractionWithCommentValidator : AbstractValidator<AddInteractionWithCommentCommand>
    {
        private readonly ICommentServices CommentServices;
        private readonly IUserServices UserServices;
        private readonly IPostServices PostServices;
        public AddInteractionWithCommentValidator(IUserServices UserServices, ICommentServices CommentServices, IPostServices PostServices)
        {
            this.CommentServices = CommentServices;
            this.PostServices = PostServices;
            this.UserServices = UserServices;
            ValidateUserIsFound();
            ValidatePostIsFound();
            ValidateCommentId();
        }

        public void ValidateUserIsFound()
        {
            RuleFor(U => U.UserId)
                .MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User Is Not Found");
        }
        public void ValidatePostIsFound()
        {
            RuleFor(P => P.PostId)
                .MustAsync(async (Key, CancelationToken) => await PostServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Post Is Not Found");
        }
        public void ValidateCommentId()
        {
            RuleFor(C => C.CommentId)
                .MustAsync(async (Key, CancelationToken) => await CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Comment Is Not Found");
        }
    }
}
