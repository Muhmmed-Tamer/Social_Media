using Social_Media.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Posts
{
    public class ImageOrVideoPath
    {
        public int Id { get; set; }
        [ForeignKey("ImageOrVideoPost")]
        public int PostId { get; set; }
        public string Image_Or_VideoPath { get; set; }
        public virtual ImageOrVideoPost? ImageOrVideoPost { get; set; }
    }
}
