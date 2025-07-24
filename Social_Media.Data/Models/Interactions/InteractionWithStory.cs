using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Interactions
{
    public class InteractionWithStory
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public InteractionType InteractionBy { get; set; }
        [ForeignKey("Story")]
        public int? StoryId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public virtual Story.Story? Story { get; set; }
    }
}
