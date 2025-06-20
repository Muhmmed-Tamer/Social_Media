﻿using Social_Media.Data.Identity;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Models
{
    public class Comment
    {

        public int Id { get; set; }
        [MaxLength(5000)]
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        // Properties  that help in RelationShips
        public virtual TextPost? TextPost { get; set; }
        public virtual ImageOrVideoPost? ImageOrVideoPost { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual List<InteractionWithComment>? likes { get; set; }
        public virtual ICollection<InteractionNotificationByComment>? InteractionNotificationByComments { get; set; }
    }
}
