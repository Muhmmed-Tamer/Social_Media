using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Models
{
    public  class DeleteInteractionWithPostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int PostId { get; set; }

       public DeleteInteractionWithPostCommand() { }

        public DeleteInteractionWithPostCommand(int id, int postId)
        {
            Id = id;
            PostId = postId;
        }


    }
}
