using FluentValidation;
using Social_Media.Core.Features.Friends.Commands.Models;
using Social_Media.Core.Implementation_UnitOFWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Friends.Commands.Validators
{
    internal class DeleteFriendValidator : AbstractValidator<DeleteFriendCommand>
    {
        private readonly UnitOFWork unitOFWork;
        
        public DeleteFriendValidator(UnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateTheFriendThatDeletedIsExist();
        }

        public void ValidateTheFriendThatDeletedIsExist() 
        {
            RuleFor(x => x.Friend_UserId).MustAsync(async (Key, CancellationToken) =>
            await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key) is not null)
                .WithMessage("The User Who Want To Delete Not Exist");




        }




    }
}
