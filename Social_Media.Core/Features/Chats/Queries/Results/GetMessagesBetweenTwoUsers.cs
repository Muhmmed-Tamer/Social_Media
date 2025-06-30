namespace Social_Media.Core.Features.Chats.Queries.Results
{
    public class GetMessagesBetweenTwoUsers
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public List<string>? MediaPaths { get; set; } = new List<string>();
    }
}
