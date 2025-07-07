using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Comments.Commands.Models
{
    public class UpdateCommentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } 

        public string UserIdWhoWantToUpdate { get; set; } 
        public int PostId { get; set; }
        public string Content { get; set; } 


        
        public UpdateCommentCommand() { }

        public UpdateCommentCommand(int id, string userId, int postId, string content)
        {
            Id = id;
            UserIdWhoWantToUpdate = userId;
            PostId = postId;
            Content = content;  
        }
    }
}
