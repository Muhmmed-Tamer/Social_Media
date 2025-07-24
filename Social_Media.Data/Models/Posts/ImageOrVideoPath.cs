using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Posts
{
    public class ImageOrVideoPath
    {
        public int Id { get; set; }
        [ForeignKey("Posts")]
        public int PostId { get; set; }
        public string Image_Or_VideoPath { get; set; }
        public virtual Post? Posts { get; set; }
        public bool IsDeleted { get; set; } =false;    
        public bool IsUpdated { get; set; } =false; 
    }
}
