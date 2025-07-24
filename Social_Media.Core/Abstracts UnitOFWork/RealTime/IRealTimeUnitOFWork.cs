using Social_Media.RealTimeServices.ImplementationHubServices.CommentsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithCommentHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithPostHub;
using Social_Media.RealTimeServices.ImplementationHubServices.MessageHub;
using Social_Media.RealTimeServices.ImplementationHubServices.NotificationsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.PostsHub;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface IRealTimeUnitOFWork
    {
        public InteractionWithCommentHubServices InteractionWithCommentHubService { get; }
        public InteractionWithPostHubServices InteractionWithPostHubService { get; }
        public NotificationHubServices NotificationHubService { get; }
        public CommentHubServices CommentHubService { get; }
        public MessageHubServices MessageHubService { get; }
        public PostHubServices PostHubService { get; }
    }
}
