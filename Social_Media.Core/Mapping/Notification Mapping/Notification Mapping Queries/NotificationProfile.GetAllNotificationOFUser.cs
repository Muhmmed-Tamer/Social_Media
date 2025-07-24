using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.Notification_Mapping
{
    public partial class NotificationProfile
    {
        public void MappingGetAllNotificationOFUser()
        {
            CreateMap<Notification, GetNotificationOFUser>()
                .ForMember(d => d.Id, Opt => Opt.MapFrom(S => S.Id))
                .ForMember(d => d.CommentIdThatInteractWith, Opt => Opt.MapFrom(S => S.InteractionNotificationByComments.FirstOrDefault().CommentId))
                .ForMember(d => d.StoryIdThatInteractWith, Opt => Opt.MapFrom(S => S.InteractionNotificationByStories.FirstOrDefault().StoryId))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserIdWhoCausedTheNotificationToBeSent))
                .ForMember(d => d.PostIdThatInteractWith, Opt => Opt.MapFrom(S => S.InteractionNotificationByPosts.FirstOrDefault().PostId))
                .ForMember(d => d.NewFriendRequestIdThatSent, Opt => Opt.MapFrom(S => S.SendFriendRequestNotifications.FirstOrDefault().Id))
                .ForMember(d => d.CommentIdThatAdded, Opt => Opt.MapFrom(S => S.CommentNotifications.FirstOrDefault().CommentId))
                .ForMember(d => d.NewMessageSent, Opt => Opt.MapFrom(S => S.MessageNotifications.FirstOrDefault().MessageId))
                .ForMember(d => d.PostIdThatAdded, Opt => Opt.MapFrom(S => S.PostNotifications.FirstOrDefault().PostId))
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(S => S.NotificationType))
                .ReverseMap();
        }
    }
}
