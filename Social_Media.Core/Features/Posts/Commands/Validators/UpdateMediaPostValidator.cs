using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    internal class UpdateMediaPostValidator : AbstractValidator<UpdateMediaPostCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateMediaPostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateNewContentIsNotEmpty();
            ValidateUSerWhoWantUpdate();
            ValidatePostExistence();
        }
        public void ValidatePostExistence()
        {
            RuleFor(P => P.PostId).MustAsync(async (Key, CancellationToken) =>
            {
                var Result = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key);
                return Result is not null && !Result.IsDeleted;

            }).WithMessage("The Post Not Exist");

        }
        public void ValidateUSerWhoWantUpdate()
        {
            RuleFor(P => P).MustAsync(async (P, CancellationToken) =>
            {
                var Post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(P.PostId);
                return Post.UserId == P.UserId ? true : false;
            }).WithMessage("You Are Not The Owner");
        }
        public void ValidateNewContentIsNotEmpty()
        {
            RuleFor(P => P.Media).NotEmpty().WithMessage("New Content Must Not Be Empty!");
            RuleFor(P => P.PostId).NotEmpty().WithMessage("PostId Must Not Be Empty!");
        }
    }

}


