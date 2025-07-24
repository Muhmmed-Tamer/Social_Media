using MediatR;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Story.Commands.Models
{
    internal class AddStoryCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public Privacy Privacy { get; set; }
        public string? Content { get; set; }
        public StoryType StoryType { get; set; }    
        public AddStoryCommand() { }    
        public AddStoryCommand(Privacy _privacy,string _UserId,string? _Content, StoryType type) {
         this.Privacy = _privacy;   
         this.UserId = _UserId; 
         this.Content = _Content;
         this.StoryType = type;
                
                
                
         }



           
    }
    
}
