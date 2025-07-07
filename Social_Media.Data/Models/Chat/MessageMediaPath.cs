using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Chat
{
    public class MessageMediaPath
    {
        public int Id { get; set; }
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public string MediaPath { get; set; }

     public bool IsDeleted { get; set; }=false;
        public virtual Message? Message { get; set; }
    }
}
