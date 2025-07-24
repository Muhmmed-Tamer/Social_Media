using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityUser;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class IdentityUnitOFWork : IIdentityUnitOFWork
    {
        public IdentityUnitOFWork(IUserRefreshTokenServices UserRefreshTokenService, IRoleServices RoleServices, IUserServices UserServices)
        {
            this.UserRefreshTokenService = UserRefreshTokenService;
            this.RoleServices = RoleServices;
            this.UserServices = UserServices;
        }

        public IUserRefreshTokenServices UserRefreshTokenService { get; }
        public IRoleServices RoleServices { get; }
        public IUserServices UserServices { get; }
    }
}
