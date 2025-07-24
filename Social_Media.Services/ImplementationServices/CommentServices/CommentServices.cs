using Serilog;
using Social_Media.Data.Models.Comments;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.CommentRepository;
using Social_Media.Services.AbstractsServices.CommentServices;

namespace Social_Media.Services.ImplementationServices.CommentServices
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

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int PostId)
        {
            try
            {
                return await CommentRepository.GetCommentsByPostIdAsync(PostId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting comments for post with ID {PostId}", PostId);
                throw;
            }
        }

        public async Task<int> GetPostIdByCommentId(int CommentId)
        {
            try
            {
                return await CommentRepository.GetPostIdByCommentId(CommentId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
