using Social_Media.Core.Features.Posts.Commands.Models;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Mapping.AddNewPost_Mapping
{
    public partial class AddNewPostProfile
    {
        public void MappingAddNotificationOFNewPost()
        {
            CreateMap<AddTextPostCommand, Notification>()
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(__ => Data.Enums.NotificationType.AddPost))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(__ => "New Post Created"))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserId_That_Want_To_AddPost))
                .ReverseMap();
        }

        public void MappingAddNotificationOFNewPostForImageOrVideo()
        {
            CreateMap<AddImageOrVideoPostCommand, Notification>()
                .ForMember(d => d.NotificationType, Opt => Opt.MapFrom(__ => Data.Enums.NotificationType.AddPost))
                .ForMember(d => d.NotificationContent, Opt => Opt.MapFrom(__ => "New Post Created"))
                .ForMember(d => d.UserIdWhoCausedTheNotificationToBeSent, Opt => Opt.MapFrom(S => S.UserId_That_Want_To_AddPost))
                .ReverseMap();
        }
    }
}
