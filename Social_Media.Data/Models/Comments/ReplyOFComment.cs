using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Comments
{
    public class ReplyOFComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [ForeignKey("UserThatWriteAComment")]
        public string UserIdThatWriteAComment { get; set; }
        [ForeignKey("UserThatWriteAReplyOFComment")]
        public string UserIdThatWriteAReplyOFComment { get; set; }
        public bool IsDeleted { get; set; } = false;
        // Navigation properties
        public virtual Comment? Comment { get; set; }
        public virtual ApplicationUser? UserThatWriteAComment { get; set; }
        public virtual ApplicationUser? UserThatWriteAReplyOFComment { get; set; }
    }
}
