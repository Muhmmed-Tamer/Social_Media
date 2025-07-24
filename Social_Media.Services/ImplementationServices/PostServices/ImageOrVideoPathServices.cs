using Serilog;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.ImplementationRepositories.PostsRepository;
using Social_Media.Services.AbstractsServices.PostsServices;

namespace Social_Media.Services.ImplementationServices.PostServices
{
    public class ImageOrVideoPathServices : Services<ImageOrVideoPath>, IImageOrVideoPathServices
    {
        private readonly ImageOrVideoPathRepository ImageOrVideoPathRepository;
        private readonly ILogger Logger;

        public ImageOrVideoPathServices(ILogger Logger, IRepository<ImageOrVideoPath> Repository, ImageOrVideoPathRepository ImageOrVideoPathRepository) : base(Logger, Repository)
        {
            this.ImageOrVideoPathRepository = ImageOrVideoPathRepository;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<ImageOrVideoPath> ImageOrVideoPaths)
        {
            try
            {
                await ImageOrVideoPathRepository.BulkInsertAsync(ImageOrVideoPaths);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
