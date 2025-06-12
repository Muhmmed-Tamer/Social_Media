using Microsoft.Extensions.DependencyInjection;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServices.InteractionsServices;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServices.InteractionsServices;
namespace Social_Media.Services.DependencyInjectionOFServices
{
    public static class ModuleServicesDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<ICommentServices, CommentServices>();
            Services.AddScoped<IFriendServices, FriendServices>();
            Services.AddScoped<IMessageServices, MessageServices>();
            Services.AddScoped<IPostServices, PostServices>();

            #region Interactions
            Services.AddScoped<IInteractionWithCommentServices, InteractionWithCommentServices>();
            Services.AddScoped<IInteractionWithPostServices, InteractionWithPostServices>();
            Services.AddScoped<IInteractionWithStoryServices, InteractionWithStoryServices>();
            #endregion

            #region Notifications
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            Services.AddScoped<IMessageNotificationServices, MessageNotificationServices>();
            Services.AddScoped<IPostNotificationServices, PostNotificationServices>();
            #endregion

            #region Interaction Notifications
            Services.AddScoped<IInteractionNotificationByStoryServices, InteractionNotificationByStoryServices>();
            Services.AddScoped<IInteractionNotificationByPostServices, InteractionNotificationByPostServices>();
            Services.AddScoped<IInteractionNotificationByCommentServices, InteractionNotificationByCommentServices>();
            #endregion

        }
    }
}
