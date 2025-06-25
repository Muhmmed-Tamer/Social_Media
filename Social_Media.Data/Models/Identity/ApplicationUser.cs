using Microsoft.AspNetCore.Identity;
using Social_Media.Data.Enums;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Models_That_Inherit_From;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Posts;
using Social_Media.Data.Models.Story;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Data.Identity
{

    public class ApplicationUser : IdentityUser
    {
        public Basic_Person_Data? Name { get; set; }
        [MaxLength(450)]
        public string? DescriptionOFProfile { get; set; }
        public string? PicturePath { get; set; }

        [MaxLength(10)]
        public GenderType Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsBlocked { get; set; } = false;
        public string? ResetPasswordCode { get; set; }

        #region Relation Between ApplicationUser & Other Classes
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<InteractionWithPost>? InteractionsWithPost { get; set; }
        public virtual ICollection<InteractionWithComment>? InteractionsWithComment { get; set; }
        public virtual ICollection<InteractionWithStory>? InteractionsWithStory { get; set; }
        public virtual ICollection<Message>? SentMessages { get; set; }
        public virtual ICollection<Message>? ReceiveMessages { get; set; }
        //public virtual ICollection<PostNotification>? PostNotifications { get; set; }
        //public virtual ICollection<MessageNotification>? MessageNotifications { get; set; }
        //public virtual ICollection<InteractionNotificationByComment>? InteractionNotificationByComments { get; set; }
        //public virtual ICollection<InteractionNotificationByPost>? InteractionNotificationByPosts { get; set; }
        //public virtual ICollection<InteractionNotificationByStory>? InteractionNotificationByStories { get; set; }
        public virtual ICollection<FriendRequest>? SendFriendRequests { get; set; }
        public virtual ICollection<FriendRequest>? ReceiveFriendRequests { get; set; }
        public virtual ICollection<Friend>? FriendshipsInitiated { get; set; }
        public virtual ICollection<Friend>? FriendshipsReceived { get; set; }
        public virtual ICollection<Story>? Stories { get; set; }
        public virtual ICollection<UserConnection>? UserConnections { get; set; }
        public virtual ICollection<Notification>? UserThatCausedNotifications { get; set; }
        public virtual ICollection<Notification>? UserThatReceiveNotifications { get; set; }
        #endregion

    }
}

