using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.Models.Story
{
    public class Story
    {
        public int Id { get; set; }
        public DateTimeOffset AddStoryAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset RemovedAt { get; set; } = DateTimeOffset.UtcNow.AddDays(1);
       
        public string? Content { get; set; }
        public StoryType StoryType { get; set; } =StoryType.Text;
        public bool IsRemoved => DateTimeOffset.UtcNow >= RemovedAt;
        [ForeignKey("User")]
        public string UserId { get; set; }
        public bool IsUpdated { get; set; } = false;
        public Privacy Privacy { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public ICollection<InteractionNotificationByStory>? InteractionNotificationByStories { get; set; }
    }
}
