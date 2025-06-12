using Social_Media.Data.Identity;
using Social_Media.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications.Interactions_Notifications
{
    public class InteractionNotificationByPost
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
