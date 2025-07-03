using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Friend_Request.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Friend_Request.Commands.Validators
{
    public class DeleteFriendRequestValidator : AbstractValidator<DeleteFriendRequestCommand>
    {
        private readonly IUnitOFWork unitOFWork;

        public DeleteFriendRequestValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateExistence();

        }

        public void ValidateExistence()
        {
            RuleFor(x => x.Id).MustAsync(async (Key, CancellationToken) =>
            {

                var friendRequest = await unitOFWork.FriendUnitOFWork.FriendRequestServices.GetByIdAsync(Key);
                return friendRequest is not null;

            }).WithMessage("This Friend Request Not Exist");
        }

    }
}
