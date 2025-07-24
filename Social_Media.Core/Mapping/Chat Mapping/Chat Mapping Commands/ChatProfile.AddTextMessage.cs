using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Chat;

namespace Social_Media.Core.Mapping.Chat_Mapping
{
    public partial class ChatProfile
    {
        public void MappingAddTextMessage()
        {
            CreateMap<AddTextMessageCommand, Message>()
                .ForMember(d => d.SenderId, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.ReceiverId, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Content))
                .ForMember(d => d.MessageType, Opt => Opt.MapFrom(_ => MessageType.Text))
                .ReverseMap();
        }
    }
}
