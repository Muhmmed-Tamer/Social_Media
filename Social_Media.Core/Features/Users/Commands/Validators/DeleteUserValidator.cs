using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Media.Core.Response_Structure;
namespace Social_Media.Core.Features.Users.Commands.Validators
{
    internal class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        private readonly IUnitOFWork unitOFWork;


        public DeleteUserValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateUserIsExist();
            ValidateUserPassword();
        }

        private void ValidateUserIsExist()
        {

            RuleFor(x => x.UserId).MustAsync(async (Key, CancellationToken) => {


                var user = await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key);

                if (user != null&&!user.IsDeleted) return true;
                else return false;
                
                
                }
            ).WithMessage("This User Already Not Exist");
        }


        private void ValidateUserPassword()
        {
            RuleFor(x => x.Password).MustAsync(async (command, Password, context, CancellationToken) =>
            {

                var user = await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(command.UserId);


                bool check = await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.CheckPasswordAsync(user!, Password);

                return check;
            }).WithMessage("The password is incorrect");

        }
    }
}
