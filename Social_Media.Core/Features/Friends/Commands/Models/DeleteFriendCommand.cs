using Social_Media.Core.Response_Structure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Friends.Commands.Models
{
    public  class DeleteFriendCommand : IRequest<Response<string>>
    {

        public int Id { get; set; } 
        public string Main_UserId { get; set; }
        public string Friend_UserId { get; set; }
        public DeleteFriendCommand(int Id,string main_UserId, string friend_UserId)
        {
            Main_UserId = main_UserId;
            Friend_UserId = friend_UserId;
        }
    }
}
