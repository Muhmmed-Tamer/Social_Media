using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Commands.Models;

namespace Social_Media.Core.Features.Comments.Commands.Validators
{
    public class DeleteCommentValidator : AbstractValidator<DeleteCommentCommand>
    {
        private readonly IUnitOFWork unitOFWork;



        public DeleteCommentValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;

            ValidateCommentIsFound();
            validateTheOwnerOfThePostOrComment();
            ValidatePostIsNotDeleted();
        }



        public void ValidateCommentIsFound()
        {
            RuleFor(s => s.Id)
                .MustAsync(async (key, cancellationToken) =>
                    await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(key) is not null)
                .WithMessage("Comment Is Not Found");
        }
        public void validateTheOwnerOfThePostOrComment()
        {
            RuleFor(c => c.PostId).MustAsync(async (command, postId, context, cancellation) =>
            {
                var post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(postId);
                var comment = await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(command.Id);

                bool check = (post.UserId == command.UserIdHowWantToDelete || comment.UserId == command.UserIdHowWantToDelete) ? true : false;

                return check;

            }

            ).WithMessage("You are not the Owner");
        }
        public void ValidatePostIsNotDeleted()
        {
            RuleFor(P => P.PostId)
                 .MustAsync(async (Key, CancelationToken) =>
                 {
                     var Post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key);
                     return !Post.IsDeleted;
                 }).WithMessage("Post Not Found");
        }

    }
}
