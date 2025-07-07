using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Core.Implementation_UnitOFWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Chats.Commands.Validators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public UpdateMessageValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateThatUserIsTHeSender();
            ValidateSenderUserAndReceiverUserIsFound();
            ValidateMessageIsFound();
        }

        public void ValidateSenderUserAndReceiverUserIsFound()
        {
            RuleFor(M => M.SenderId)
                .MustAsync(async (Key, CancelationToken) => await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)

                .WithMessage("Sender User Is Not Found");
            RuleFor(M => M.ReceiverId)
                .MustAsync(async (Key, CancelationToken) => await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key) is not null ? true : false)
                .WithMessage("Receiver User Is Not Found");
        }
        public void ValidateThatUserIsTHeSender()
        {
            RuleFor(c => c).MustAsync(async (c, CancellationToken) =>
            {
                if (c.UserWhoWantToDelete != c.SenderId) return false;
                else return true;

            }).WithMessage("You are not The Sender");
        }

        public void ValidateMessageIsFound()
        {
            RuleFor(M => M.Id).MustAsync(async (Key, CancellationToken) =>
            {

                var result = await unitOFWork.ChatUnitOFWork.MessageServices.GetByIdAsync(Key);

                if (result != null && result.IsDeleted != true) return true;

                else return false;
            })
                .WithMessage("Message Is Not Found");
        }

    }
}
