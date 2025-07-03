using Social_Media.Data.Models.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications.Interactions_Notifications
{
    public class InteractionNotificationByPost
    {
        public int Id { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }
        public virtual Post? Post { get; set; }
        public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
