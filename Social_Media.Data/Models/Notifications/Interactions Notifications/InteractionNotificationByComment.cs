using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications.Interactions_Notifications
{
    public class InteractionNotificationByComment
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual Social_Media.Models.Comment? Comment { get; set; }
    }
}
