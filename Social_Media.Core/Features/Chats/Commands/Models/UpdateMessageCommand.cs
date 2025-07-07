using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class UpdateMessageCommand : IRequest<Response<string>>
    {
    
     public int  Id { get; set; }       
     public string Content { get; set; }

     public string ReceiverId { get; set; }
     public string SenderId { get; set; }
     public string UserWhoWantToDelete { get; set; }    

        public UpdateMessageCommand() { }
     
     public UpdateMessageCommand(int id,string content,string senderId,string receiverId,string userWhoWantToDelete)
        {
            Id = id; Content = content; ReceiverId = senderId; SenderId = receiverId; this.UserWhoWantToDelete = userWhoWantToDelete;   
        }
       


    }
}
