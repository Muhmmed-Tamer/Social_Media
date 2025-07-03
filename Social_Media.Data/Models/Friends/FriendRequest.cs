using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Friends
{
    public class FriendRequest
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsRead { get; set; } = false;
        public bool IsCanceled { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public Status status { get; set; } = Status.Pending;
        public DateTime SendAt { get; set; } = DateTime.Now;
        [ForeignKey("FriendUser")]
        public string FriendUserId { get; set; }
        public virtual ApplicationUser? FriendUser { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
