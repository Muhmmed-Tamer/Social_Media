using FluentValidation;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Users.Commands.Models;

namespace Social_Media.Core.Features.Users.Commands.Validators
{
    public class UpdateUserNameValidator : AbstractValidator<UpdateUserNameCommand>
    {

        private readonly IUnitOFWork unitOFWork;

        public UpdateUserNameValidator(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
            ValidateUserIsFound();
            ValidateNewNameIsNotEmpty();
            ValidateName();

        }

        public void ValidateUserIsFound()
        {

            RuleFor(x => x.UserId).MustAsync(async (Key, CancellationToken) =>
            {
                var User = await unitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(Key);
                return (User is not null && !User.IsDeleted && !User.IsBlocked) ? true : false;

            }).WithMessage("User Not Found");
        }
        public void ValidateName()
        {
            RuleFor(x => x.New_Name).MustAsync(async (Key, CancellationToken) =>
            {
                return ((Key.FirstNameInEnglish != null && Key.LastNameInEnglish != null) || (Key.FirstNameInAabic != null && Key.LastNameInEnglish != null)) ? true : false;
            }
                ).WithMessage("Some required name fields are null");

        }
        public void ValidateNewNameIsNotEmpty()
        {
            RuleFor(U => U.New_Name.FirstNameInEnglish).NotEmpty().WithMessage("First Name In English Must Not Be Empty!");
            RuleFor(U => U.New_Name.LastNameInEnglish).NotEmpty().WithMessage("Last Name In English Must Not Be Empty!");
            RuleFor(U => U.New_Name.FirstNameInAabic).NotEmpty().WithMessage("First Name In Arabic Must Not Be Empty!");
            RuleFor(U => U.New_Name.LastNameInArabic).NotEmpty().WithMessage("Last Name In Arabic Must Not Be Empty!");
        }
    }
}
