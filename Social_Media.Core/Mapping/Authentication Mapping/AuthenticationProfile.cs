using AutoMapper;

namespace Social_Media.Core.Mapping.Authentication_Mapping
{
    public partial class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            MappingUserRefreshTokenToJWTTokenResult();
            MappingAddUserRefreshToken();
        }
    }
}
