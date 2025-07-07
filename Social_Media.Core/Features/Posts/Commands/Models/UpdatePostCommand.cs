using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public  class UpdatePostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Content { get; set; } 

        public string UserId { get; set; }  
        public Privacy Privacy { get; set; }    
        

        public UpdatePostCommand() { }
        public UpdatePostCommand(int id, string content ,string userId,Privacy privacy)
        {
            Id = id; Content = content; UserId = userId; Privacy = privacy;
        }
      

    }
}
