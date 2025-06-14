using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models
{
    public class Story
    {
        public int Id { get; set; }
        public DateTimeOffset AddStoryAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset RemovedAt { get; set; } = DateTimeOffset.UtcNow.AddDays(1);
        public string? StoryPath { get; set; }
        public string? Description { get; set; }
        public bool IsRemoved => DateTimeOffset.UtcNow >= RemovedAt;
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Privacy Privacy { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public ICollection<InteractionNotificationByStory>? InteractionNotificationByStories { get; set; }
    }
}
