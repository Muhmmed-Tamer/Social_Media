using Social_Media.Core.Features.Users.Queries.Results;
using Social_Media.Data.Identity;

namespace Social_Media.Core.Mapping.User_Mapping
{
    public partial class UserProfile
    {
        public void MappingGetUserQuery()
        {
            CreateMap<ApplicationUser, GetUser>()
                .ForMember(d => d.UserId, Opt => Opt.MapFrom(S => S.Id))
                .ForMember(d => d.UserName, Opt => Opt.MapFrom(S => S.Name.FirstNameInEnglish + " " + S.Name.LastNameInEnglish))
                .ForMember(d => d.PhoneNumber, Opt => Opt.MapFrom(S => S.PhoneNumber))
                .ForMember(d => d.BirthOFDate, Opt => Opt.MapFrom(S => S.BirthDate))
                .ForMember(d => d.Email, Opt => Opt.MapFrom(S => S.Email))
                .ForMember(d => d.Gender, Opt => Opt.MapFrom(S => S.Gender))
                .ReverseMap();
        }
    }
}
