using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Http;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public  class DeleteTextPostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } 

        public string UserIdWhoWantToDelete { get; set; }   

        public DeleteTextPostCommand() { }
        public DeleteTextPostCommand(int id, string UserId) 
        { 
            Id = id;
            UserIdWhoWantToDelete = UserId; 
        }   
    }
}
