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
    internal class UpdateMediaPostValidator : AbstractValidator<UpdateMediaPostCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateMediaPostValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateUSerWhoWantUpdate();
            ValidatePostExistence();

        }
        public void ValidatePostExistence()
        {
            RuleFor(c => c.Id).MustAsync(async (Key, CancellationToken) =>
            {
                var result = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(Key);
                return result != null && !result.IsDeleted;

            }).WithMessage("The Post Not Exist");

        }
        public void ValidateUSerWhoWantUpdate()
        {
            RuleFor(x => x).MustAsync(async (x, CancellationToken) =>
            {
                var post = await unitOFWork.PostUnitOFWork.PostServices.GetByIdAsync(x.Id);
                if (post.UserId != null && post.UserId == x.UserId) return true;
                else return false;

            }).WithMessage("You Are Not The Owner");
        }




    }

}


