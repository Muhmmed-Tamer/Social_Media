using Social_Media.Data.Identity;
using Social_Media.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Notifications
{
    public class FriendRequestNotification
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;

        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("Friend")]
        public int FriendId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Friend? Friend { get; set; }
    }
}
