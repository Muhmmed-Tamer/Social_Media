using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Implementation_UnitOFWork;
using Social_Media.Core.PipeLineBehavior;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.FriendsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServices.StoryServices;
using Social_Media.Services.AbstractsServices.UserConnectionServices;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServices.FriendServices;
using Social_Media.Services.ImplementationServices.InteractionsServices;
using Social_Media.Services.ImplementationServices.PostServices;
using Social_Media.Services.ImplementationServices.StoryServices;
using Social_Media.Services.ImplementationServices.UserConnectionServices;
using System.Reflection;


namespace SchoolProject.Core.DependencyInjectionOFCore
{
    public static class ModuleCoreDependencies
    {
        public static void AddCoreDependencies(this IServiceCollection Services)
        {
            Services.AddMediatR(CF => CF.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddScoped<IUnitOFWork, UnitOFWork>();
            Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #region UserConnection
            Services.AddScoped<IUserConnectionServices, UserConnectionServices>();
            #endregion

            #region Registration OF  Message and Friend            
            Services.AddScoped<IMessageServices, MessageServices>();
            Services.AddScoped<IFriendServices, FriendServices>();
            #endregion

            #region Posts
            Services.AddScoped<IPostServices, PostServices>();
            Services.AddScoped<IImageOrVideoPathServices, ImageOrVideoPathServices>();
            #endregion

            #region Story
            Services.AddScoped<IImageOrVideoStoryPathService, ImageOrVideoStoryPathService>();
            Services.AddScoped<IStoryService, StoryService>();
            #endregion

            #region Registration OF Interactions
            Services.AddScoped<IInteractionWithStoryServices, InteractionWithStoryServices>();
            Services.AddScoped<IInteractionWithPostServices, InteractionWithPostServices>();
            Services.AddScoped<IInteractionWithCommentServices, InteractionWithCommentServices>();
            #endregion

            #region Registration OF Notifications
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            Services.AddScoped<IMessageNotificationServices, MessageNotificationServices>();
            Services.AddScoped<IFriendRequestServices, FriendRequestServices>();
            #endregion

            #region Registration OF Interactions Notifications
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            Services.AddScoped<IInteractionNotificationByPostServices, InteractionNotificationByPostServices>();
            Services.AddScoped<IInteractionNotificationByCommentServices, InteractionNotificationByCommentServices>();
            #endregion
        }
    }
}
