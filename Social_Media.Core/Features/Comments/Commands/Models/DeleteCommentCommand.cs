
using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Comments.Commands.Models
{
    public class DeleteCommentCommand : IRequest<Response<string>>   
    {
        public int Id { get; set; }
        public int PostId { get; set; } 
      

        public string UserIdHowWantToDelete { get; set; }
   
        public DeleteCommentCommand() { }   

        public DeleteCommentCommand(int id, int postId,string userId)
        {
            this.Id = id;
            this.PostId = postId;
            this.UserIdHowWantToDelete = userId;    
            
        }   
    }
}
