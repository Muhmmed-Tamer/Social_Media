using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Notifications.Queries.Results
{
    public class GetNotificationOFUser
    {
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string UserIdWhoCausedTheNotificationToBeSent { get; set; }
        public int? PostIdThatInteractWith { get; set; }
        public int? CommentIdThatInteractWith { get; set; }
        public int? StoryIdThatInteractWith { get; set; }
        public int? PostIdThatAdded { get; set; }
        public int? CommentIdThatAdded { get; set; }
        public int? NewFriendRequestIdThatSent { get; set; }
        public int? NewMessageSent { get; set; }

    }
}
