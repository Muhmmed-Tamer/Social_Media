using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class UpdateUserPhotoValidator : AbstractValidator<UpdateUserPhotoCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateUserPhotoValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateUserIsFound();
            ValidatePhotoNotNull();
        }
        public void ValidateUserIsFound()
        {

            RuleFor(x => x.UserId).MustAsync(async (Key, CancellationToken) =>
            {
                var user = await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key);
                if (user != null && !user.IsDeleted)
                    return true;
                else return false;

            }).WithMessage("User Not Found");
        }

        public void ValidatePhotoNotNull()
        {
            RuleFor(x => x.Photo)
                .Must(photo => photo != null)
                .WithMessage("Photo is required.");
        }
    }
}
