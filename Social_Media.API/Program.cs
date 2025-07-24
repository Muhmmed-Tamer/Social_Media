using ConstantStatementInAllProject.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
using Social_Media.Services.AbstractsServices.IdentityServices.IdentityRole;
using Social_Media.Services.DependencyInjectionOFServices;
using System.Text;

namespace Social_Media.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.System.IO.DirectoryNotFoundException: 'C:\Users\LOQ\Desktop\Social_Media\Social_Media.API\wwwroot\'ms/aspnetcore/swashbuckle
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

            //JWT Bearer
            builder.Services.AddAuthentication(
                    Options =>
                    {
                        Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
            ).AddJwtBearer(
                    Options =>
                    {
                        Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["JWT:Issuer"],
                            ValidAudience = builder.Configuration["JWT:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]!)),
                            ClockSkew = TimeSpan.Zero
                        };
                    }
            );

            //Configuration Of Swagger
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "School Project",
                    Description = "Description",
                    Contact = new OpenApiContact()
                    {
                        Name = "Muhmmed Tamer",
                        Email = "muhmmedelshnawy@gmail.com",
                        Url = new Uri("https://mydomain.com")
                    }
                });

                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT Key"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                        {
                           new OpenApiSecurityScheme()
                           {
                              Reference = new OpenApiReference()
                              {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                              },
                              Name = "Bearer",
                              In = ParameterLocation.Header
                           },
                           new List<string>()
                        }
                    });
            });
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            using (var Scope = app.Services.CreateScope())
            {
                IRoleServices RoleServices = Scope.ServiceProvider.GetRequiredService<IRoleServices>();
                await RoleServices.AddRolesToSystemWhenProgramIsStart(RolesConstants.RolesInSystem);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerRequestDelegate>();
            app.MapHub<HubNegotiation>("/ConnectionHub");

            app.MapControllers();
            app.Run();
        }
    }
}
