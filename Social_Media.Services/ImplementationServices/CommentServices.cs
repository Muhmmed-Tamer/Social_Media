using Serilog;
using Social_Media.Data.Models.Comments;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.Services.AbstractsServices;

namespace Social_Media.Services.ImplementationServices
{
    public class CommentServices : Services<Comment>, ICommentServices
    {
        private readonly ICommentRepository CommentRepository;
        private readonly ILogger Logger;
        public CommentServices(ILogger Logger, IRepository<Comment> Repository, ICommentRepository CommentRepository) : base(Logger, Repository)
        {
            this.CommentRepository = CommentRepository;
            this.Logger = Logger;
        }

        public Task<bool> CommentIsExistInAllCommentsOfPost(List<Comment> AllComments, int CommentId)
        {
            try
            {
                return CommentRepository.CommentIsExistInAllCommentsOfPost(AllComments, CommentId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while checking if comment exists in all comments of post");
                throw;
            }
        }

        public Task<List<Comment>> GetCommentsByPostIdAsync(int PostId)
        {
            try
            {
                return CommentRepository.GetCommentsByPostIdAsync(PostId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting comments for post with ID {PostId}", PostId);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
