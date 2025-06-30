using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Data.Models.Chat;

namespace Social_Media.Core.Mapping.Chat_Mapping
{
    public partial class ChatProfile
    {
        public void MappingAddAudioMessage()
        {
            CreateMap<AddAudioMessageCommand, Message>()
                .ForMember(d => d.SenderId, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.ReceiverId, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.MessageType, Opt => Opt.MapFrom(_ => Data.Enums.MessageType.Audio))
                .ReverseMap();
        }
    }
}
