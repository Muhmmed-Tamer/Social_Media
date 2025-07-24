using Social_Media.Data.Models.Chat;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications
{
    public class MessageNotification
    {
        public int Id { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }
        public virtual Message? Message { get; set; }
        public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
