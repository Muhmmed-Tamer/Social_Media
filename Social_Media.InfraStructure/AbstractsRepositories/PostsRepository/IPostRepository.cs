using Social_Media.Data.Models.Posts;

namespace Social_Media.InfraStructure.AbstractsRepositories.PostsRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetPostsOFUserAsync(string UserId);
        Task<Post> GetPostWithImageOrVideoPathsById(int PostId);
    }
}
