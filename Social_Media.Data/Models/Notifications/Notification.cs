using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications
{
    public class Notification
    {
        public int Id { get; set; }
        [ForeignKey("UserThatCausedNotification")]
        public string UserIdWhoCausedTheNotificationToBeSent { get; set; }
        [ForeignKey("UserThatReceivedNotification")]
        public string UserIdWhoReceivedTheNotification { get; set; }
        public NotificationType NotificationType { get; set; }
        public string? NotificationContent { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool IsRead { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public virtual ApplicationUser? UserThatCausedNotification { get; set; }
        public virtual ApplicationUser? UserThatReceivedNotification { get; set; }

        public virtual ICollection<InteractionNotificationByPost>? InteractionNotificationByPosts { get; set; }
        public virtual ICollection<InteractionNotificationByComment>? InteractionNotificationByComments { get; set; }
        public virtual ICollection<InteractionNotificationByStory>? InteractionNotificationByStories { get; set; }
        public virtual ICollection<MessageNotification>? MessageNotifications { get; set; }
        public virtual ICollection<PostNotification>? PostNotifications { get; set; }
        public virtual ICollection<CommentNotification>? CommentNotifications { get; set; }
        public virtual ICollection<SendFriendRequestNotification>? SendFriendRequestNotifications { get; set; }
        public virtual ICollection<ConfirmFriendRequestNotification>? ConfirmFriendRequestNotifications { get; set; }
    }
}
