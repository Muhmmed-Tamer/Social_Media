using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Models
{
    public class DeleteInteractionWithCommentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }

        public DeleteInteractionWithCommentCommand() { }
        public DeleteInteractionWithCommentCommand(int id, int postId, int commentId)
        {
            Id = id;
            PostId = postId;
            CommentId = commentId;
        }

    }
}
