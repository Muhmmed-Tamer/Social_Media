using FluentValidation;
using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Features.Comments.Commands.Validators
{
    public class AddCommentToPostValidator : AbstractValidator<AddCommentToPostCommand>
    {
        private readonly IUserServices UserServices;
        private readonly IPostServices PostServices;
        public AddCommentToPostValidator(IUserServices UserServices, IPostServices PostServices)
        {
            this.UserServices = UserServices;
            this.PostServices = PostServices;
            ValidateCommentContent();
            ValidatePostIsFound();
            ValidateUserIsFound();
            ValidatePostIsNotDeleted();
        }

        public void ValidateCommentContent()
        {
            RuleFor(C => C.Comment_Content)
                .NotEmpty().WithMessage("Comment Content can not be Empty")
                .NotNull().WithMessage("Comment Content can not be Null");
        }
        public void ValidatePostIsFound()
        {
            RuleFor(C => C.PostId).MustAsync(async (Key, CancelationToken) => await PostServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Post Is Not Found");
        }
        public void ValidateUserIsFound()
        {
            RuleFor(C => C.UserId).MustAsync(async (Key, CancelationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User Is Not Found");
        }
        public void ValidatePostIsNotDeleted()
        {
            RuleFor(P => P.PostId)
                 .MustAsync(async (Key, CancelationToken) =>
                 {
                     var Post = await PostServices.GetByIdAsync(Key);
                     return !Post.IsDeleted;
                 }).WithMessage("Post Not Found");
        }
    }
}
