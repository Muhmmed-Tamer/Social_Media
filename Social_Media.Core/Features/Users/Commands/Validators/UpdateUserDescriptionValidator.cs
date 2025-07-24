using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class UpdateUserDescriptionValidator : AbstractValidator<UpdateUserDescriptionCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public UpdateUserDescriptionValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateUserIsFound();
            ValidateDescriptionNotNull();
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

        public void ValidateDescriptionNotNull()
        {
            RuleFor(x => x.Description).Must((Description, CancellationToken) => Description is not null ? true : false).WithMessage("Description Can't Be Null");
        }

    }
}
