using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Identity;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Models;
using System.Reflection;

namespace Social_Media.Data
{
    public class ContextData : IdentityDbContext<ApplicationUser>
    {
        public ContextData(DbContextOptions<ContextData> Options) : base(Options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        #region Classes That Mapped Tables In DataBase
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<InteractionWithPost> InteractionWithPosts { get; set; }
        public virtual DbSet<InteractionWithComment> InteractionWithComments { get; set; }
        public virtual DbSet<InteractionWithStory> InteractionWithStories { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<PostNotification> PostNotifications { get; set; }
        public virtual DbSet<MessageNotification> MessageNotifications { get; set; }
        public virtual DbSet<FriendRequestNotification> FriendRequestNotifications { get; set; }
        public virtual DbSet<InteractionNotificationByComment> InteractionNotificationByComments { get; set; }
        public virtual DbSet<InteractionNotificationByPost> InteractionNotificationByPosts { get; set; }
        public virtual DbSet<InteractionNotificationByStory> InteractionNotificationByStories { get; set; }
        public virtual DbSet<TextPost> TextPosts { get; set; }
        public virtual DbSet<ImageOrVideoPost> ImageOrVideoPosts { get; set; }
        public virtual DbSet<UserConnection> UserConnections { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        #endregion
    }
}
