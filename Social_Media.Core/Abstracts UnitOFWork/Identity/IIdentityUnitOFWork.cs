using Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IIdentityUnitOFWork
    {
        public IUserRefreshTokenServices UserRefreshTokenService { get; }
        public IRoleServices RoleServices { get; }
        public IUserServices UserServices { get; }
    }
}
