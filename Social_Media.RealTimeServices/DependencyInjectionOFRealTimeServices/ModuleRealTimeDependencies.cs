using Microsoft.Extensions.DependencyInjection;
using Social_Media.RealTimeServices.ImplementationHubServices.CommentsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithCommentHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithPostHub;
using Social_Media.RealTimeServices.ImplementationHubServices.MessageHub;
using Social_Media.RealTimeServices.ImplementationHubServices.NotificationsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.PostsHub;

namespace Social_Media.RealTimeServices.DependencyInjectionOFRealTimeServices
{
    public static class ModuleRealTimeDependencies
    {
        public static void AddRealTimeServices(this IServiceCollection Services)
        {
            Services.AddScoped<PostHubServices>();
            Services.AddScoped<CommentHubServices>();
            Services.AddScoped<NotificationHubServices>();
            Services.AddScoped<InteractionWithPostHubServices>();
            Services.AddScoped<InteractionWithCommentHubServices>();
            Services.AddScoped<MessageHubServices>();
        }
    }
}
