namespace ConstantStatementInAllProject.Roles
{
    public static class RolesConstants
    {
        public const string Admin = "Administrator";
        public const string User = "RegularUser";
        public const string SuperAdmin = "SuperAdministrator";

        public static readonly IReadOnlyDictionary<string, string> RolesInSystem = new Dictionary<string, string>()
        {
            {"Admin",Admin},
            {"User",User},
            {"SuperAdmin",SuperAdmin}
        }.AsReadOnly();

    }
}