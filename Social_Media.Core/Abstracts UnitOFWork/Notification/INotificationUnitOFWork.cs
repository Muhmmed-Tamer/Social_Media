using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository.InteractionsNotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices.INotificationsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Core.Abstracts_UnitOFWork
{
    public interface INotificationUnitOFWork
    {
        public INotificationServices NotificationServices { get; }
        public IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService { get; }
        public ISendFriendRequestNotificationServices SendFriendRequestNotificationService { get; }
        public ICommentNotificationServices CommentNotificationService { get; }
        public IMessageNotificationServices MessageNotificationServices { get; }
        public IPostNotificationServices PostNotificationServices { get; }
        public IInteractionNotificationByCommentServices InteractionNotificationByCommentServices { get; }
        public IInteractionNotificationByStoryServices InteractionNotificationByStoryServices { get; }
        public IInteractionNotificationByPostServices InteractionNotificationByPostServices { get; }
    }
}
