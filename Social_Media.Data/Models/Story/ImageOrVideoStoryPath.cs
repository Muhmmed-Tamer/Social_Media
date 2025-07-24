using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Media.Data.Models.Story
{
    public class ImageOrVideoStoryPath
    {
        public int Id { get; set; }
        [ForeignKey("Story")]
        public int StoryId { get; set; }
        public string Image_Or_VideoPath { get; set; }
        public Story? Story { get; set; }
        public bool IsUpdated { get; set; } = false;
    }
}
