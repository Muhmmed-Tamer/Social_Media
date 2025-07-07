using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Comments.Commands.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateCommentValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidatePostIsNotDeleted();
            ValidateCommentIsFound();
            ValidateTheOwnerOfTheComment();


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
        public void ValidateCommentIsFound()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("This Comment Not Exist");
        }
        public void ValidateTheOwnerOfTheComment()
        {
            RuleFor(x => x).MustAsync(async (x, CancellationToken) =>
            {
                var comment = await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(x.Id);
                if (comment.UserId == x.UserIdWhoWantToUpdate) return true;
                else return false;  

            }).WithMessage("You are not the owner!");
        }


    }
}
