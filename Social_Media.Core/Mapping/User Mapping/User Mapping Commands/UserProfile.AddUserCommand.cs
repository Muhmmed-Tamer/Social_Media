using Social_Media.Core.Features.Users.Commands.Models;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Mapping.User_Mapping
{
    public partial class UserProfile
    {
        public void MappingAddUserCommand()
        {
            CreateMap<AddUserCommand, ApplicationUser>()
                .ForPath(d => d.Name.FirstNameInEnglish, opt => opt.MapFrom(s => s.User_FirstName_In_English))
                .ForPath(d => d.Name.FirstNameInAabic, opt => opt.MapFrom(s => s.User_FirstName_In_Arabic))
                .ForPath(d => d.Name.LastNameInEnglish, opt => opt.MapFrom(s => s.User_LastName_In_English))
                .ForPath(d => d.Name.LastNameInArabic, opt => opt.MapFrom(s => s.User_LastName_In_Arabic))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User_Email))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.User_Mobile))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.User_BirthDate))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.User_Gender))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User_Email))
                .ReverseMap();
        }
    }
}
