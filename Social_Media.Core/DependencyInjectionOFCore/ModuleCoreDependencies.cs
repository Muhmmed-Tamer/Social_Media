using Microsoft.Extensions.DependencyInjection;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Implementation_UnitOFWork;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServices.InteractionsServices;
using Social_Media.Services.ImplementationServices.PostServices;
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

            #region Registration OF  Message and Friend            
            Services.AddScoped<IMessageServices, MessageServices>();
            Services.AddScoped<IFriendServices, FriendServices>();
            #endregion
            #region Posts
            Services.AddScoped<ITextPostServices, TextPostServices>();
            Services.AddScoped<IImageOrVideoPostServices, ImageOrVideoPostServices>();
            Services.AddScoped<IImageOrVideoPathServices, ImageOrVideoPathServices>();
            #endregion
            #region Registration OF Interactions
            Services.AddScoped<IInteractionWithStoryServices, InteractionWithStoryServices>();
            Services.AddScoped<IInteractionWithPostServices, InteractionWithPostServices>();
            Services.AddScoped<IInteractionWithCommentServices, InteractionWithCommentServices>();
            #endregion
            #region Registration OF Notifications
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            Services.AddScoped<IMessageNotificationServices, MessageNotificationServices>();
            Services.AddScoped<IFriendRequestNotificationServices, FriendRequestNotificationServices>();
            #endregion
            #region Registration OF Interactions Notifications
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            Services.AddScoped<IInteractionNotificationByPostServices, InteractionNotificationByPostServices>();
            Services.AddScoped<IInteractionNotificationByCommentServices, InteractionNotificationByCommentServices>();
            #endregion
        }
    }
}
