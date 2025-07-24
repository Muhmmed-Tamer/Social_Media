namespace Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole
{
    public interface IRoleServices
    {
        Task AddRolesToSystemWhenProgramIsStart(IReadOnlyDictionary<string, string> KeyValueRoles);
    }
}
