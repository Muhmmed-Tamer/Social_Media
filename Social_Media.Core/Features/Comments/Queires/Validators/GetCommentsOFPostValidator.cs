using FluentValidation;
using Social_Media.Core.Features.Comments.Queires.Models;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Core.Features.Comments.Queires.Validators
{
    public class GetCommentsOFPostValidator : AbstractValidator<GetCommentsOFPostQuery>
    {
        private readonly IPostServices PostServices;
        public GetCommentsOFPostValidator(IPostServices PostServices)
        {
            this.PostServices = PostServices;
        }
        public void ValidateGetCommentsOFPostQuery()
        {
            RuleFor(C => C.PostId)
                .NotEmpty().WithMessage("PostId is required.")
                .NotNull().WithMessage("PostId cannot be null.");

            RuleFor(P => P.PostId)
                .MustAsync(async (Key, CancelationToken) => await PostServices.GetByIdAsync(Key) is not null)
                .WithMessage("Post Is Not Found");
        }
    }
}
