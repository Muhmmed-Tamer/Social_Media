using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.ImplementationRepositories;
using Social_Media.Models;

namespace Social_Media.InfraStructure.DependencyInjectionOFInfraStructure
{
    public static class ModuleInfraStructureDependencies
    {
        public static void AddInfraStructureDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            Services.AddScoped<IRepository<Friend>, Repository<Friend>>();
            Services.AddScoped<IRepository<TextPost>, Repository<TextPost>>();
            Services.AddScoped<IRepository<ImageOrVideoPost>, Repository<ImageOrVideoPost>>();
            Services.AddScoped<IRepository<PostNotification>, Repository<PostNotification>>();
            Services.AddScoped<IRepository<Message>, Repository<Message>>();

            #region Notifications
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            Services.AddScoped<IRepository<FriendRequestNotification>, Repository<FriendRequestNotification>>();
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            #endregion

            #region Interactions
            Services.AddScoped<IRepository<InteractionWithComment>, Repository<InteractionWithComment>>();
            Services.AddScoped<IRepository<InteractionWithPost>, Repository<InteractionWithPost>>();
            Services.AddScoped<IRepository<InteractionWithStory>, Repository<InteractionWithStory>>();
            #endregion

            Services.AddScoped<IRepository<InteractionNotificationByPost>, Repository<InteractionNotificationByPost>>();
            Services.AddScoped<IRepository<InteractionNotificationByComment>, Repository<InteractionNotificationByComment>>();
            Services.AddScoped<IRepository<InteractionNotificationByStory>, Repository<InteractionNotificationByStory>>();
        }
    }
}
