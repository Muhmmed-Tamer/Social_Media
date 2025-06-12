using Microsoft.Extensions.DependencyInjection;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.ImplementationRepositories;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.DependencyInjectionOFInfraStructure
{
    public static class ModuleInfraStructureDependencies
    {
        public static void AddInfraStructureDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<ICommentRepository, CommentRepository>();
            Services.AddScoped<IFriendRepository, FriendRepository>();
            Services.AddScoped<IPostRepository, PostRepository>();
            Services.AddScoped<IPostNotificationRepository, PostNotificationRepository>();
            Services.AddScoped<IMessageRepository, MessageRepository>();

            #region Notifications
            Services.AddScoped<IMessageNotificationRepository, MessageNotificationRepository>();
            Services.AddScoped<IFriendRequestNotificationRepository, FriendRequestNotificationRepository>();
            Services.AddScoped<IMessageNotificationRepository, MessageNotificationRepository>();
            #endregion

            #region Interactions
            Services.AddScoped<IInteractionWithCommentRepository, IInteractionWithCommentRepository>();
            Services.AddScoped<IInteractionWithPostRepository, IInteractionWithPostRepository>();
            Services.AddScoped<IInteractionWithStoryRepository, IInteractionWithStoryRepository>();
            #endregion

            Services.AddScoped<IInteractionNotificationByPostRepository, InteractionNotificationByPostRepository>();
            Services.AddScoped<IInteractionNotificationByCommentRepository, InteractionNotificationByCommentRepository>();
            Services.AddScoped<IInteractionNotificationByStoryRepository, InteractionNotificationByStoryRepository>();
        }
    }
}
