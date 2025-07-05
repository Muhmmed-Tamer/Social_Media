using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    public class DeletePostValidator : AbstractValidator<DeletePostCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public DeletePostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidatePost();
            validateTheOwnerOfThePost();
        }


        private void ValidatePost()
        {
            RuleFor(s => s.Id)
               .MustAsync(async (key, cancellationToken) =>
                   await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(key) is not null)
               .WithMessage("Post Is Not Found");
        }

        private void validateTheOwnerOfThePost()
        {
            RuleFor(x => x.Id).MustAsync(async (command, postId, context, cancellation) =>
            {
                var post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(postId);
                bool check = (command.UserIdWhoWantToDelete == post.UserId) ? true : false;

                return check;
            }).WithMessage("You are not the Owner");
        }

    }
}
