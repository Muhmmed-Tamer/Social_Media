using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Friend_Request.Commands.Models
{
    public class DeleteFriendRequestCommand : IRequest<Response<string>>
    {
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendUserId { get; set; }

        public DeleteFriendRequestCommand() { }

        public DeleteFriendRequestCommand(int id, string userId, string friendUserId)
        {
            Id = id;
            UserId = userId;
            FriendUserId = friendUserId;
        }
    }
}
