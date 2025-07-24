using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.RealTimeServices.ImplementationHubServices.CommentsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithCommentHub;
using Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithPostHub;
using Social_Media.RealTimeServices.ImplementationHubServices.MessageHub;
using Social_Media.RealTimeServices.ImplementationHubServices.NotificationsHub;
using Social_Media.RealTimeServices.ImplementationHubServices.PostsHub;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class RealTimeUnitOFWork : IRealTimeUnitOFWork
    {
        public RealTimeUnitOFWork(InteractionWithCommentHubServices InteractionWithCommentHubService, InteractionWithPostHubServices InteractionWithPostHubService, NotificationHubServices NotificationHubService, CommentHubServices CommentHubService, MessageHubServices MessageHubService, PostHubServices PostHubService)
        {
            this.InteractionWithCommentHubService = InteractionWithCommentHubService;
            this.InteractionWithPostHubService = InteractionWithPostHubService;
            this.NotificationHubService = NotificationHubService;
            this.CommentHubService = CommentHubService;
            this.MessageHubService = MessageHubService;
            this.PostHubService = PostHubService;
        }

        public InteractionWithCommentHubServices InteractionWithCommentHubService { get; }

        public InteractionWithPostHubServices InteractionWithPostHubService { get; }

        public NotificationHubServices NotificationHubService { get; }

        public CommentHubServices CommentHubService { get; }

        public MessageHubServices MessageHubService { get; }

        public PostHubServices PostHubService { get; }
    }
}
