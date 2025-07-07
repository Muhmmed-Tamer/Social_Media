using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Chats.Commands.Models
{
    public class DeleteMessageCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } 

        public string SenderId { get; set; }    

        public string ReceiverId { get; set; }  
        
        public string UserWhoWantToDelete { get; set; } 
        public DeleteMessageCommand() { }
        public DeleteMessageCommand(int id, string senderId, string receiverId,string userWhoWantToDelete)
        {
            Id = id;
            SenderId = senderId;
            ReceiverId = receiverId;
            this.UserWhoWantToDelete = userWhoWantToDelete;         
        }   
    }
}
