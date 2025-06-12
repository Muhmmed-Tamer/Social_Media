using Social_Media.Data.Identity;
using Social_Media.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications
{
    public class MessageNotification
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Message")]
        public int MessageId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Message? Message { get; set; }
    }
}
