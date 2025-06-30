using Social_Media.Data.Models.Posts;

namespace Social_Media.InfraStructure.AbstractsRepositories.PostsRepository
{
    public interface IImageOrVideoPathRepository : IRepository<ImageOrVideoPath>
    {
        Task BulkInsertAsync(List<ImageOrVideoPath> ImageOrVideoPaths);
    }
}
