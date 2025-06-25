using AutoMapper;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Core.Mapping.Post_Mapping
{
    public partial class PostProfile : Profile
    {
        public PostProfile()
        {
            MappingAddTextPostCommand();
            MappingImageOrVideoPostCommand();
            MappingGetTextPostQuery();
        }
        private readonly IProtocolAndHostServices ProtocolAndHostServices;
        public PostProfile(IProtocolAndHostServices ProtocolAndHostServices)
        {
            this.ProtocolAndHostServices = ProtocolAndHostServices;
        }
    }
}
