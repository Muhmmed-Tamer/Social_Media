using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Validator
{
    public class DeleteInteractionWithCommentValidator : AbstractValidator<DeleteInteractionWithCommentCommand>
    {
        private readonly IUnitOFWork unitOFWork;
        public DeleteInteractionWithCommentValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateCommentExistence();
            ValidateExistence();
        }


        public void ValidateExistence()
        {
            RuleFor(I => I.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage("Interaction With Comment Not Exist");
        }
        public void ValidateCommentExistence()
        {
            RuleFor(C => C.CommentId).MustAsync(async (Key, CancellationToken) => await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(Key) is not null ? true : false)
                .WithMessage(" This Comment For This Interaction Not Exist");

            RuleFor(C => C.CommentId)
                .MustAsync(async (Key, Cancelation) =>
                {
                    var Comment = await unitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(Key);
                    return !Comment.IsDeleted;
                }).WithMessage("Comment Not Found");
        }
    }
}
