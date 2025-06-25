using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models.Logging
{
    [NotMapped]
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string? Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
        public string? UserId { get; set; }
        public string? Method { get; set; }
        public string? IpAddress { get; set; }
    }
}
