namespace Social_Media.Core.Features.Comments.Queires.Results
{
    public class ReplyOFCommentQuery
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserIdThatWriteAComment { get; set; }
        public string UserIdThatWriteAReplyOFComment { get; set; }
    }
}
