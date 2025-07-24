using MediatR;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Comments.Commands.Models
{
    public class UpdateCommentCommand : IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public string UserIdWhoWantToUpdate { get; set; }
        public string New_Content { get; set; }
        public UpdateCommentCommand() { }

        public UpdateCommentCommand(int Id, string UserId, string New_Content)
        {
            this.CommentId = Id;
            this.UserIdWhoWantToUpdate = UserId;
            this.New_Content = New_Content;
        }
    }
}
