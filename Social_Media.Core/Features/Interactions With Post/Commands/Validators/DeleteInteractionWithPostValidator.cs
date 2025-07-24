using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Validators
{
    public class DeleteInteractionWithPostValidator : AbstractValidator<DeleteInteractionWithPostCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public DeleteInteractionWithPostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidatePostExistence();
            ValidateInteractionExistence();
        }

        public void ValidatePostExistence()
        {
            RuleFor(x => x.PostId).MustAsync(async (Key, CancellationToken) => await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key) is not null ? true : false)
                 .WithMessage("The Post Not Exist");

        }
        public void ValidateInteractionExistence()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) => await unitOFWork.InteractionUnitOFWork.InteractionWithPostServices.GetByIdAsync(Key) is not null ? true : false)
                 .WithMessage("The Interaction Not Exist");

            RuleFor(P => P.PostId)
                .MustAsync(async (Key, Cancelation) =>
                {
                    var Post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key);
                    return !Post.IsDeleted;
                });
        }

    }
}
