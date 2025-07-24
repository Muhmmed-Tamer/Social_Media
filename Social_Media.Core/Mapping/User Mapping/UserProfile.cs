using AutoMapper;

namespace Social_Media.Core.Mapping.User_Mapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            MappingAddUserCommand();
            MappingGetUserQuery();
        }
    }
}
