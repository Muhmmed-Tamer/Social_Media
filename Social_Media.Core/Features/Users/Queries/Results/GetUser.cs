using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Users.Queries.Results
{
    public class GetUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime BirthOFDate { get; set; }
        public GenderType Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
