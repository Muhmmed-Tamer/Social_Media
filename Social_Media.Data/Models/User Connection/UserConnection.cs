using Social_Media.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Media.Data.Models
{
    public class UserConnection
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
