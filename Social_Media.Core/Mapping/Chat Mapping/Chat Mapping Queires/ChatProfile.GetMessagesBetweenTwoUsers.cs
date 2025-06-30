using Social_Media.Core.Features.Chats.Queries.Results;
using Social_Media.Data.Models.Chat;

namespace Social_Media.Core.Mapping.Chat_Mapping
{
    public partial class ChatProfile
    {
        public void MappingGetMessagesBetweenTwoUsers()
        {
            CreateMap<Message, GetMessagesBetweenTwoUsers>()
                .ForMember(d => d.SenderId, Opt => Opt.MapFrom(S => S.SenderId))
                .ForMember(d => d.ReceiverId, Opt => Opt.MapFrom(S => S.ReceiverId))
                .ForMember(d => d.Content, Opt => Opt.MapFrom(S => S.Content))
                .ForMember(d => d.MediaPaths, Opt => Opt.MapFrom(S => S.MessageMediaPaths.Select(MP => MP.MediaPath)))
                .ForMember(d => d.SentAt, Opt => Opt.MapFrom(S => S.CreatedAt))
                .ReverseMap();
        }
    }
}
