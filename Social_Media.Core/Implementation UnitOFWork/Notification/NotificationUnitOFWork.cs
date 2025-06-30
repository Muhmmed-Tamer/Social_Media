using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.AbstractsServices.INotificationsServices;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddCommentNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.AddPostNotification;
using Social_Media.Services.AbstractsServices.INotificationsServices.FriendRequest_Notification;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class NotificationUnitOFWork : INotificationUnitOFWork
    {
        public NotificationUnitOFWork(INotificationServices NotificationServices, IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService, ISendFriendRequestNotificationServices SendFriendRequestNotificationService, ICommentNotificationServices CommentNotificationService, IMessageNotificationServices MessageNotificationServices, IPostNotificationServices PostNotificationServices)
        {
            this.NotificationServices = NotificationServices;
            this.ConfirmFriendRequestNotificationService = ConfirmFriendRequestNotificationService;
            this.SendFriendRequestNotificationService = SendFriendRequestNotificationService;
            this.CommentNotificationService = CommentNotificationService;
            this.MessageNotificationServices = MessageNotificationServices;
            this.PostNotificationServices = PostNotificationServices;
        }

        public INotificationServices NotificationServices { get; }

        public IConfirmFriendRequestNotificationServices ConfirmFriendRequestNotificationService { get; }

        public ISendFriendRequestNotificationServices SendFriendRequestNotificationService { get; }

        public ICommentNotificationServices CommentNotificationService { get; }

        public IMessageNotificationServices MessageNotificationServices { get; }

        public IPostNotificationServices PostNotificationServices { get; }
    }
}
