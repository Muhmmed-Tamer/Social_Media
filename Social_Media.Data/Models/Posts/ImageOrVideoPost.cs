using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Models
{

    public class ImageOrVideoPost
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Privacy Privacy { get; set; }

        // Properties  that help in RelationShips
        public List<Comment>? Comments { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<InteractionNotificationByPost>? InteractionNotificationByPosts { get; set; }
        public virtual ICollection<ImageOrVideoPath>? ImageOrVideoPaths { get; set; }
    }
}
