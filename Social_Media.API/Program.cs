using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.DependencyInjectionOFCore;
using Serilog;
using Social_Media.Core.Handle_Error_RequestDelegate;
using Social_Media.Data;
using Social_Media.Data.Helpers.DataFromappSettings.Email;
using Social_Media.Data.Helpers.DataFromappSettings.JWT;
using Social_Media.Data.Helpers.DataFromappSettings.SMS;
using Social_Media.Data.Identity;
using Social_Media.InfraStructure.DependencyInjectionOFInfraStructure;
using Social_Media.RealTimeServices.DependencyInjectionOFRealTimeServices;
using Social_Media.RealTimeServices.Hub_Negotiation;
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

            builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("Email"));
            builder.Services.Configure<SMSSetting>(builder.Configuration.GetSection("SMS"));


            builder.Services.AddCoreDependencies();
            builder.Services.AddInfraStructureDependencies();
            builder.Services.AddServiceDependencies();
            builder.Services.AddRealTimeServices();
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

            builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

            builder.Services.AddCors(S => S.AddPolicy("MYCORS1", builder =>
            {
                builder.WithOrigins("http://127.0.0.1:5500")
                       .AllowCredentials()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            builder.Services.AddCors(S => S.AddPolicy("MYCORS2", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("SignalRCors", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:3000") // Add all your client origins
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Required for SignalR
                });
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("SignalRCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerRequestDelegate>();
            app.MapHub<HubNegotiation>("/ConnectionHub");

            app.MapControllers();
            app.Run();
        }
    }
}
