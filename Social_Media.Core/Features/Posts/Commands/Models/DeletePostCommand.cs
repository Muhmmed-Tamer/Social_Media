using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class DeletePostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string UserIdWhoWantToDelete { get; set; }

        public DeletePostCommand() { }
        public DeletePostCommand(int id, string UserId)
        {
            Id = id;
            UserIdWhoWantToDelete = UserId;
        }
    }
}
