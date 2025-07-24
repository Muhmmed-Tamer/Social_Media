using AutoMapper;

namespace Social_Media.Core.Mapping.AddNewCommentNotification_Mapping
{
    public partial class AddNewCommentNotificationProfile : Profile
    {
        public AddNewCommentNotificationProfile()
        {
            MappingAddNewCommentNotificationCommand();
        }
    }
}
