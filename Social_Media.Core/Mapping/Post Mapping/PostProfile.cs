using AutoMapper;

namespace Social_Media.Core.Mapping.Post_Mapping
{
    public partial class PostProfile : Profile
    {
        public PostProfile()
        {
            MappingAddTextPostCommand();
            MappingAddImageOrVideoPostCommand();
        }
    }
}
