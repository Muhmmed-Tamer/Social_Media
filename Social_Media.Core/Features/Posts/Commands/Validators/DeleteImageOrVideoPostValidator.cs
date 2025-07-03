using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    public class DeleteImageOrVideoPostValidator : AbstractValidator<DeleteImageOrVideoPostCommand>
    {
        private readonly IUnitOFWork unitOFWork;    

        public DeleteImageOrVideoPostValidator() { }
        public DeleteImageOrVideoPostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateImageOrVideoPost();
        }

        private void ValidateImageOrVideoPost()
        {

            RuleFor(s => s.Id)
               .MustAsync(async (key, cancellationToken) =>
                   await unitOFWork.PostUnitOFWork.ImageOrVideoPathServices.GetByIdAsync(key) is not null)
               .WithMessage("Post Is Not Found");

        }

        





        

    }
}
