using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Models_That_Inherit_From;

namespace Social_Media.Core.Features.Users.Commands.Models
{
    public class UpdateUserNameCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public Basic_Person_Data New_Name { get; set; }

        public UpdateUserNameCommand() { }
        public UpdateUserNameCommand(string UserId, Basic_Person_Data NewName)
        {
            this.New_Name = NewName;
            this.UserId = UserId;
        }
    }
}
