using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Posts.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Posts.Commands.Validators
{
    public class DeleteTextPostValidator : AbstractValidator<DeleteTextPostCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public DeleteTextPostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateTextPost();
            validateTheOwnerOfThePost();
        }


        private void ValidateTextPost()
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
