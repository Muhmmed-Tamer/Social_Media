using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Models_That_Inherit_From;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public  class UpdateUserNameCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public Basic_Person_Data Name { get; set; }

        public UpdateUserNameCommand() { }
        public UpdateUserNameCommand(string userId, Basic_Person_Data name)
        {
            UserId = userId;
            Name = name;
        }
    }
}
