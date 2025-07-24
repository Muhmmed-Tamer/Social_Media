using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Caption { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public Privacy Privacy { get; set; }
        public PostType PostType { get; set; }

        // Properties  that help in RelationShips
        public List<Comment>? Comments { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<InteractionNotificationByPost>? InteractionNotificationByPosts { get; set; }
        public virtual ICollection<ImageOrVideoPath>? ImageOrVideo_Paths { get; set; }
        public virtual ICollection<InteractionWithComment>? InteractionWithComments { get; set; }
    }
}
