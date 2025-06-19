using Microsoft.AspNetCore.Http;
using Social_Media.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Core.Features.Story.Commands.Models
{
    internal class AddImageOrVideoStoryCommand
    {
        public string UserId { get; set; }
        public Privacy Privacy { get; set; }
        public string? Content { get; set; }
        public StoryType StoryType { get; set; }
        public IFormFile ImageOrVideo { get; set; } 
        public AddImageOrVideoStoryCommand() { }
        public AddImageOrVideoStoryCommand(string userId, Privacy privacy, string? content, StoryType storyType, IFormFile imageOrVideo)
        {
            UserId = userId;
            Privacy = privacy;
            Content = content;
            StoryType = storyType;
            ImageOrVideo = imageOrVideo;
        }
    }
}
