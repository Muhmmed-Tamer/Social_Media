using Microsoft.AspNetCore.Identity;
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole;

namespace Social_Media.Services.ImplementationServices.IdentityServices.IdentityRole
{
    public class RoleServices : IRoleServices
    {
        private RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> ManagerRole { get; }
        private readonly Serilog.ILogger Logger;
        public RoleServices(Serilog.ILogger Logger, RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> ManagerRole)
        {
            this.ManagerRole = ManagerRole;
            this.Logger = Logger;
        }
        public async Task AddRolesToSystemWhenProgramIsStart(IReadOnlyDictionary<string, string> KeyValueRoles)
        {
            try
            {
                foreach (var KeyValueRole in KeyValueRoles)
                {
                    Microsoft.AspNetCore.Identity.IdentityRole Role = new Microsoft.AspNetCore.Identity.IdentityRole()
                    {
                        Name = KeyValueRole.Value
                    };
                    await ManagerRole.CreateAsync(Role);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
