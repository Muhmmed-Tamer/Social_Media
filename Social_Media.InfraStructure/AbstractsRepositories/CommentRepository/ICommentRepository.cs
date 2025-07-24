using Social_Media.Data.Models.Comments;

namespace Social_Media.InfraStructure.AbstractsRepositories.CommentRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int PostId);
        Task<bool> CommentIsExistInAllCommentsOfPost(List<Comment> AllComments, int CommentId);
        Task<int> GetPostIdByCommentId(int CommentId);
    }
}
