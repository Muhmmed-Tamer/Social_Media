using Social_Media.Core.Response_Structure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class DeleteImageOrVideoPostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
     

        public DeleteImageOrVideoPostCommand() { }


        public DeleteImageOrVideoPostCommand(int id)
        {
            Id = id;
           
        }  
    }
}
