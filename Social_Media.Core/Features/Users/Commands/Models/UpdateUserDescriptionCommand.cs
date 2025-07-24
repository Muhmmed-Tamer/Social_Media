using MediatR;
using Social_Media.Core.Response_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class UpdateUserDescriptionCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }

        public string Description { get; set; }
         public UpdateUserDescriptionCommand() { }
        public UpdateUserDescriptionCommand(string userId, string description)
        {
            UserId = userId;
            Description = description;
        }
    }
}
