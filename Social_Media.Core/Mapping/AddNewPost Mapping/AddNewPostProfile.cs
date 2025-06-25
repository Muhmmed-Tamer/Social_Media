using AutoMapper;

namespace Social_Media.Core.Mapping.AddNewPost_Mapping
{
    public partial class AddNewPostProfile : Profile
    {
        public AddNewPostProfile()
        {
            MappingAddNotificationOFNewPost();
            MappingAddNotificationOFNewPostForImageOrVideo();
        }
    }
}
