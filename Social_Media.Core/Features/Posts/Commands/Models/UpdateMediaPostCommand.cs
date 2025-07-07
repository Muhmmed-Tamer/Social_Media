using MediatR;
using Microsoft.AspNetCore.Http;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Posts;
using Social_Media.Data.Models.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Posts.Commands.Models
{
    public class UpdateMediaPostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } 
        public string Content { get; set; }
        public string Caption { get; set; }

        public string UserId { get; set; }
        public Privacy Privacy { get; set; }
        public List<IFormFile> Media {  get; set; }
        public ICollection<ImageOrVideoPath> imageOrVideoPaths { get; set; }

        public UpdateMediaPostCommand() { }
        public UpdateMediaPostCommand(int id, string content, string caption, string userId,Privacy privacy,List<IFormFile> media)
        {
            Id = id;
            Content = content;
            Caption = caption;
            UserId = userId;
            Privacy = privacy;
            Media = media;
        
        }
    }
}
