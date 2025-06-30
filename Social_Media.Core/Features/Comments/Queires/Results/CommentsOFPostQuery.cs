namespace Social_Media.Core.Features.Comments.Queires.Results
{
    public class CommentsOFPostQuery
    {
        public int CommentId { get; set; }
        public string UserIdThatMakeCommentInPost { get; set; }
        public string CommentContentThatUserMade { get; set; }
        public DateTimeOffset MakeCommentIn { get; set; }
        public List<ReplyOFCommentQuery>? Replies { get; set; }
    }
}
