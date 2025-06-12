using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Models
{
    public class Friend
    {
        public int Id { get; set; }
        [ForeignKey("MainUser")]
        public string UserId { get; set; }

        [ForeignKey("FriendUser")]
        public string FriendUserId { get; set; }
        public Status status { get; set; }
        public DateTimeOffset SendAt { get; set; } = DateTimeOffset.UtcNow;
        public virtual ApplicationUser? FriendUser { get; set; }
        public virtual ApplicationUser? MainUser { get; set; }

    }
}
