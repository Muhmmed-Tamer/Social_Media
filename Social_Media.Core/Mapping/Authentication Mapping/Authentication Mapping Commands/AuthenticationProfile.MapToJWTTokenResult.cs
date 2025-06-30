using Social_Media.Data.Helpers.Models;
using Social_Media.Data.Models.Identity;

namespace Social_Media.Core.Mapping.Authentication_Mapping
{
    public partial class AuthenticationProfile
    {
        public void MappingUserRefreshTokenToJWTTokenResult()
        {
            CreateMap<UserRefreshToken, JWTTokenResult>()
                .ForMember(d => d.AccessToken, Opt => Opt.MapFrom(S => S.AccessToken))
                .ForMember(d => d.RefreshToken, Opt => Opt.MapFrom(S => S.RefreshToken))
                .ReverseMap();
        }
    }
}
