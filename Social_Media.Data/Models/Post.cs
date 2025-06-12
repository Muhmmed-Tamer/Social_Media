using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Models
{

    public class Post
    {
        public int Id { get; set; }
        public string? Caption { get; set; }

        public string? Content { get; set; }

        public PostType Type { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Privacy Privacy { get; set; }

        // Properties  that help in RelationShips
        public List<Comment>? Comments { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<InteractionNotificationByPost>? InteractionNotificationByPosts { get; set; }
    }
}
