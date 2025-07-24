using FluentValidation;
using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Services.AbstractsServices.CommentServices;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Features.Comments.Commands.Validators
{
    public class AddReplyOFCommentValidator : AbstractValidator<AddReplyOFCommentCommand>
    {
        private readonly ICommentServices CommentServices;
        private readonly IPostServices PostServices;
        private readonly IUserServices UserServices;
        public AddReplyOFCommentValidator(IUserServices UserServices, ICommentServices CommentServices, IPostServices PostServices)
        {
            this.CommentServices = CommentServices;
            this.UserServices = UserServices;
            this.PostServices = PostServices;
            ValidateUserAndCommentISFound();
            ValidateReplyOFComment();
        }

        public void ValidateReplyOFComment()
        {
            RuleFor(U => U.UserIdThatWriteAComment)
                .NotEmpty().WithMessage("UserIdThatWriteAComment is required.")
                .NotNull().WithMessage("UserId cannot be null.");

            RuleFor(U => U.UserIdThatWriteAReplyOFComment)
                .NotEmpty().WithMessage("UserIdThatWriteAReplyOFComment is required.")
                .NotNull().WithMessage("UserId cannot be null.");


            RuleFor(C => C.CommentId)
                .NotNull().WithMessage("CommentId cannot be null.")
                .NotEmpty().WithMessage("CommentId is required");

            RuleFor(C => C.Content)
                .NotEmpty().WithMessage("Content is required.")
                .NotNull().WithMessage("Content cannot be null.")
                .MaximumLength(500).WithMessage("Content must not exceed 500 characters.");
        }

        public void ValidateUserAndCommentISFound()
        {
            RuleFor(U => U.UserIdThatWriteAReplyOFComment)
                .MustAsync(async (Key, CancellationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("User That Write a Reply Of Comment does not exist.");

            RuleFor(U => U.UserIdThatWriteAComment)
               .MustAsync(async (Key, CancellationToken) => await UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
               .WithMessage("User That Write a Comment  does not exist.");

            RuleFor(U => U.CommentId)
                .MustAsync(async (Key, CancellationToken) => await CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Comment does not exist.");
            RuleFor(U => U.CommentId)
                .MustAsync(async (Key, Cancelation) =>
                {
                    var Comment = await CommentServices.GetByIdAsync(Key);
                    return !Comment.IsDeleted;
                }).WithMessage("Comment Is Not Found");

            RuleFor(C => C.CommentId)
                .MustAsync(async (Key, Cancelation) =>
                {
                    var PostId = await CommentServices.GetPostIdByCommentId(Key);
                    var Post = await PostServices.GetByIdAsync(PostId);
                    return !Post.IsDeleted;
                }).WithMessage("Post Is Not Found");
        }
    }
}
