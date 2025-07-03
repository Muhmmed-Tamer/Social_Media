using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Validator
{
    public class DeleteInteractionWithCommentValidator : AbstractValidator<DeleteInteractionWithCommentCommand>
    {
        private readonly IUnitOFWork unitOFWork;
        public DeleteInteractionWithCommentValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidatePostExistence();
            ValidateCommentExistence();
            ValidateExistence();
        }


        public void ValidateExistence()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Interaction With Comment Not Exist");
        }
        public void ValidatePostExistence()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage(" This Post That You Interacted With It Not Exist");
        }
        public void ValidateCommentExistence()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage(" This Comment For This Interaction Not Exist");
        }
    }
}
