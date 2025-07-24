using Microsoft.AspNetCore.Razor.TagHelpers;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Comments
{
    public class Comment
    {

        public int Id { get; set; }
        [MaxLength(5000)]
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;    
        public bool IsUpdated { get; set; } =false;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }

        // Properties  that help in RelationShips
        public virtual Post? Post { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual List<InteractionWithComment>? likes { get; set; }
        public virtual ICollection<InteractionNotificationByComment>? InteractionNotificationByComments { get; set; }
        public virtual List<ReplyOFComment>? ReplyOFComments { get; set; }
    }
}
