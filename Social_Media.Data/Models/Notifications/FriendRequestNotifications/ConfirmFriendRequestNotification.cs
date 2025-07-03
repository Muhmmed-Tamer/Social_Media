using Social_Media.Data.Models.Friends;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications.FriendRequestNotifications
{
    public class ConfirmFriendRequestNotification
    {
        public int Id { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("FriendRequest")]
        public int FriendRequestId { get; set; }
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }
        public virtual FriendRequest? FriendRequest { get; set; }
        public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        
    
    }
}
