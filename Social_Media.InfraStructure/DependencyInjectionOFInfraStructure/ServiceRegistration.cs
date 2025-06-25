using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Social_Media.InfraStructure.DependencyInjectionOFInfraStructure
{
    public static class ServiceRegistration
    {
        public static void AddInfraStructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Database Context
            Services.AddDbContext<Social_Media.Data.ContextData>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

        }
    }
}