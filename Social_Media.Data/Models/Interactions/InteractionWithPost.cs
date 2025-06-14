using Social_Media.Data.Enums;
using Social_Media.Data.Identity;
using Social_Media.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Interactions
{
    public class InteractionWithPost
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public InteractionType InteractionBy { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public virtual TextPost? TextPost { get; set; }
        public virtual ImageOrVideoPost? ImageOrVideoPost { get; set; }
    }
}
