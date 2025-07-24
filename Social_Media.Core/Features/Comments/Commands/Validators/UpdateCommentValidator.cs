using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Commands.Models;

namespace Social_Media.Core.Features.Comments.Commands.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateCommentValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateCommentIsFound();
            ValidateTheOwnerOfTheComment();
            ValidatePostIsNotDeleted();
            ValidateNewContentIsNotEmpty();
        }
        public void ValidateCommentIsFound()
        {
            RuleFor(x => x.CommentId).MustAsync(async (Key, CancellationToken) => await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("This Comment Not Exist");
        }
        public void ValidateNewContentIsNotEmpty()
        {
            RuleFor(C => C.New_Content).NotEmpty().WithMessage("New Content Must Not Be Empty!");
        }
        public void ValidatePostIsNotDeleted()
        {
            RuleFor(C => C).MustAsync(async (C, CancellationToken) =>
            {
                var Post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(await unitOFWork.CommentUnitOFWork.CommentServices.GetPostIdByCommentId(C.CommentId));
                return !Post.IsDeleted;
            }).WithMessage("This Post Is Deleted!");
        }
        public void ValidateTheOwnerOfTheComment()
        {
            RuleFor(C => C).MustAsync(async (C, CancellationToken) =>
            {
                var comment = await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(C.CommentId);
                var Post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(await unitOFWork.CommentUnitOFWork.CommentServices.GetPostIdByCommentId(C.CommentId));
                return (comment.UserId == C.UserIdWhoWantToUpdate || Post.UserId == C.UserIdWhoWantToUpdate) ? true : false;
            }).WithMessage("You are not the owner!");
        }
    }
}
