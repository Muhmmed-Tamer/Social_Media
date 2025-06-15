
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.DependencyInjectionOFCore;
using Serilog;
using Social_Media.Data;
using Social_Media.Data.Identity;
using Social_Media.InfraStructure.DependencyInjectionOFInfraStructure;
using Social_Media.Services.DependencyInjectionOFServices;

namespace Social_Media.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ContextData>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Config).CreateLogger();

            builder.Services.AddSignalR();

            builder.Services.AddCoreDependencies();
            builder.Services.AddInfraStructureDependencies();
            builder.Services.AddServiceDependencies();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               Options =>
               {
                   //Must Change When Deploying
                   //Password Settings
                   Options.Password.RequireNonAlphanumeric = false;
                   Options.Password.RequireUppercase = false;
                   Options.Password.RequireLowercase = false;
                   Options.Password.RequireDigit = false;
                   Options.Password.RequiredUniqueChars = 0;
                   Options.Password.RequiredLength = 4;
                   //LockOut Setting
                   Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                   Options.Lockout.MaxFailedAccessAttempts = 5;
                   Options.Lockout.AllowedForNewUsers = true;
                   //User Settings
                   Options.User.RequireUniqueEmail = true;
                   Options.SignIn.RequireConfirmedAccount = true;
               }).AddEntityFrameworkStores<ContextData>()
               .AddDefaultTokenProviders();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapHub<>("");// Specify the hub you want to use, e.g., app.MapHub<ChatHub>("/chatHub");

            app.MapControllers();
            app.Run();
        }
    }
}
