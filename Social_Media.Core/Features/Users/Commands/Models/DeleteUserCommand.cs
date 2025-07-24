
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Media.Core.Response_Structure;
namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {

        public string UserId { get; set; }  
        public string Password { get; set; }    

        public DeleteUserCommand(string UserId, string password)  
            { this.UserId = UserId;
             this.Password = password;  
            }   
    }
}
