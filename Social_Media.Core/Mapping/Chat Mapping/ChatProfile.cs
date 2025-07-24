using AutoMapper;

namespace Social_Media.Core.Mapping.Chat_Mapping
{
    public partial class ChatProfile : Profile
    {
        public ChatProfile()
        {
            MappingGetMessagesBetweenTwoUsers();
            MappingAddAudioMessage();
            MappingAddMediaMessage();
            MappingAddTextMessage();
        }
    }
}
