using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Validators
{
    public  class DeleteInteractionWithPostValidator : AbstractValidator<DeleteInteractionWithPostCommand>
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

        }


    }
}
