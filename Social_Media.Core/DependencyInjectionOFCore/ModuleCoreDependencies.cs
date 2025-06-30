using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Implementation_UnitOFWork;
using Social_Media.Core.PipeLineBehavior;
using Social_Media.Services.AbstractsServices.FriendsServices;
using Social_Media.Services.AbstractsServices.UserConnectionServices;
using Social_Media.Services.ImplementationServices.FriendServices;
using Social_Media.Services.ImplementationServices.UserConnectionServices;
using System.Reflection;


namespace SchoolProject.Core.DependencyInjectionOFCore
{
    public static class ModuleCoreDependencies
    {
        public static void AddCoreDependencies(this IServiceCollection Services)
        {
            #region Registration OF Interactions Notifications            
            Services.AddScoped<INotificationUnitOFWork, NotificationUnitOFWork>();
            #endregion
            #region ExternalNotification UnitOFWork
            Services.AddScoped<IExternalNotificationUnitOFWork, ExternalNotificationUnitOFWork>();
            #endregion
            #region ConfigurationOFFiles UnitOFWork
            Services.AddScoped<IConfigurationOFFilesUnitOFWork, ConfigurationOFFilesUnitOFWork>();
            #endregion
            #region Registration OF Notifications            
            Services.AddScoped<IFriendRequestServices, FriendRequestServices>();
            #endregion
            #region Registration OF Interactions            
            Services.AddScoped<IInteractionUnitOFWork, InteractionUnitOFWork>();
            #endregion
            #region Registration OF Friend            
            Services.AddScoped<IFriendUnitOFWork, FriendUnitOFWork>();
            #endregion
            #region RealTime UnitOFWork
            Services.AddScoped<IRealTimeUnitOFWork, RealTimeUnitOFWork>();
            #endregion
            #region Identity UnitOFWork
            Services.AddScoped<IIdentityUnitOFWork, IdentityUnitOFWork>();
            #endregion
            #region Fluent Validation
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            #endregion
            #region UserConnection
            Services.AddScoped<IUserConnectionServices, UserConnectionServices>();
            #endregion
            #region UnitOFWork
            Services.AddScoped<IUnitOFWork, UnitOFWork>();
            #endregion
            #region AutoMapper
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion
            #region Comment
            Services.AddScoped<ICommentUnitOFWork, CommentUnitOFWork>();
            #endregion
            #region Meditor
            Services.AddMediatR(CF => CF.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            #endregion
            #region Posts
            Services.AddScoped<IPostUnitOFWork, PostUnitOFWork>();
            #endregion
            #region Story
            Services.AddScoped<IStoryUnitOFWork, StoryUnitOFWork>();
            #endregion
            #region Chat
            Services.AddScoped<IChatUnitOFWork, ChatUnitOFWork>();
            #endregion
        }
    }
}
