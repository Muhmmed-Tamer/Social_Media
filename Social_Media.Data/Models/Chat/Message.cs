using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Chat
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [ForeignKey("SenderUser")]
        public string SenderId { get; set; }
        [ForeignKey("ReceiverUser")]
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsDeleted { get; set; } = false;    
        public bool IsUpdated { get; set; } = false;  
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public MessageType MessageType { get; set; }
        public virtual ApplicationUser? SenderUser { get; set; }
        public virtual ApplicationUser? ReceiverUser { get; set; }
        public virtual ICollection<MessageMediaPath>? MessageMediaPaths { get; set; } = new List<MessageMediaPath>();

    }
}
