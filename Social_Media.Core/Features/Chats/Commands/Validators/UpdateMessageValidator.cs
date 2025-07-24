using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Data.Models.Chat;

namespace Social_Media.Core.Features.Chats.Commands.Validators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public UpdateMessageValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateThatUserIsTHeSender();
            ValidateMessageIsFound();
        }
        public void ValidateThatUserIsTHeSender()
        {
            RuleFor(M => M).MustAsync(async (Message, CancellationToken) =>
            {
                Message message = await unitOFWork.ChatUnitOFWork.MessageServices.GetByIdAsync(Message.MessageId);
                return message.SenderId == Message.UserIdWhoWantToUpdate ? true : false;
            }).WithMessage("User Can`t Update Message Is Not Sent It");
        }

        public void ValidateMessageIsFound()
        {
            RuleFor(M => M.MessageId).MustAsync(async (Key, CancellationToken) =>
            {
                return await unitOFWork.ChatUnitOFWork.MessageServices.GetByIdAsync(Key) is null ? false : true;
            })
                .WithMessage("Message Is Not Found");
        }

    }
}
