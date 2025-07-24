using Social_Media.Data.Models.Comments;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications.AddCommentNotification
{
    public class CommentNotification
    {
        public int Id { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }
        public virtual Comment? Comment { get; set; }
        public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
