using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class UpdateUserNameValidator : AbstractValidator<UpdateUserNameCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public UpdateUserNameValidator(IUnitOFWork unitOFWork)
        {
             this.unitOFWork = unitOFWork;
            ValidateUserIsFound();
            ValidateName();

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
        public void ValidateName()
        {
            RuleFor(x => x.Name).MustAsync(async (Key, CancellationToken) =>
            {
                if ((Key.FirstNameInEnglish != null && Key.LastNameInEnglish != null) || (Key.FirstNameInAabic != null && Key.LastNameInEnglish != null))
                    return true;
                else return false;
            }
                ).WithMessage("Some required name fields are null");
            
        }






    }
}
