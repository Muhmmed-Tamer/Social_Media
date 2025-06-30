using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Media.Data.Identity;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Identity;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Logging;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
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
        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<InteractionNotificationByComment> InteractionNotificationByComments { get; set; }
        public virtual DbSet<InteractionNotificationByPost> InteractionNotificationByPosts { get; set; }
        public virtual DbSet<InteractionNotificationByStory> InteractionNotificationByStories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserConnection> UserConnections { get; set; }
        public virtual DbSet<ImageOrVideoPath> ImageOrVideoPaths { get; set; }
        public virtual DbSet<CommentNotification> CommentNotifications { get; set; }
        public virtual DbSet<SendFriendRequestNotification> SendFriendRequestNotifications { get; set; }
        public virtual DbSet<ConfirmFriendRequestNotification> ConfirmFriendRequestNotifications { get; set; }
        public virtual DbSet<MessageMediaPath> MessageMediaPaths { get; set; }
        public virtual DbSet<ReplyOFComment> ReplyOFComments { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        #endregion
    }
}
