using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Friends
{
    public class Friend
    {
        public int Id { get; set; }
        [ForeignKey("MainUser")]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }=false;

        [ForeignKey("FriendUser")]
        public string FriendUserId { get; set; }
        public DateTimeOffset FriendsSince { get; set; } = DateTimeOffset.Now;
        public virtual ApplicationUser? FriendUser { get; set; }
        public virtual ApplicationUser? MainUser { get; set; }
    }
}
