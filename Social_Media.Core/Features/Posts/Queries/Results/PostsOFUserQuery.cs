using Social_Media.Data.Enums;

namespace Social_Media.Core.Features.Posts.Queries.Results
{
    public class PostsOFUserQuery
    {
        public int PostId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Privacy Privacy { get; set; }
        public PostType PostType { get; set; }
        public string? PostContent { get; set; } = string.Empty;
        public string? PostCaption { get; set; } = string.Empty;
        public List<string>? ImageOrVideoPaths { get; set; } = new List<string>();
    }
}
