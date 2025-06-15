using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using Social_Media.Models;
using Social_Media.Services.AbstractsServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.ImplementationServices;
using Social_Media.Services.ImplementationServicesOFSpecialModels;
namespace Social_Media.Services.DependencyInjectionOFServices
{
    public static class ModuleServicesDependencies
    {
        public static void AddServiceDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<IServices<Comment>, Services<Comment>>();
            Services.AddScoped<IServices<Friend>, Services<Friend>>();
            Services.AddScoped<IServices<Message>, Services<Message>>();

            #region Posts 
            Services.AddScoped<IServices<TextPost>, Services<TextPost>>();
            Services.AddScoped<IServices<ImageOrVideoPost>, Services<ImageOrVideoPost>>();
            Services.AddScoped<IServices<ImageOrVideoPath>, Services<ImageOrVideoPath>>();
            #endregion

            #region Interactions
            Services.AddScoped<IServices<InteractionWithComment>, Services<InteractionWithComment>>();
            Services.AddScoped<IServices<InteractionWithPost>, Services<InteractionWithPost>>();
            Services.AddScoped<IServices<InteractionWithStory>, Services<InteractionWithStory>>();
            #endregion

            #region Notifications
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            Services.AddScoped<IServices<MessageNotification>, Services<MessageNotification>>();
            Services.AddScoped<IServices<PostNotification>, Services<PostNotification>>();
            #endregion

            #region Interaction Notifications
            Services.AddScoped<IServices<InteractionNotificationByStory>, Services<InteractionNotificationByStory>>();
            Services.AddScoped<IServices<InteractionNotificationByPost>, Services<InteractionNotificationByPost>>();
            Services.AddScoped<IServices<InteractionNotificationByComment>, Services<InteractionNotificationByComment>>();
            #endregion

            #region Files
            Services.AddScoped<IFileService, FileService>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostImageServices>, ConfigurationOFPostImageServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFPostVideoServices>, ConfigurationOFPostVideoServices>();
            Services.AddScoped<IFileConfigurationServices<ConfigurationOFUserImageServices>, ConfigurationOFUserImageServices>();
            #endregion

        }
    }
}
