using Microsoft.EntityFrameworkCore;
using Serilog;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;

namespace Social_Media.InfraStructure.ImplementationRepositories.PostsRepository
{
    public class ImageOrVideoPathRepository : Repository<ImageOrVideoPath>, IImageOrVideoPathRepository
    {
        private readonly DbSet<ImageOrVideoPath> ImageOrVideoPaths;
        private readonly ILogger Logger;
        public ImageOrVideoPathRepository(Data.ContextData Data, ILogger Logger) : base(Data, Logger)
        {
            this.ImageOrVideoPaths = Data.Set<ImageOrVideoPath>();
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<ImageOrVideoPath> ImageOrVideoPaths)
        {
            try
            {
                await this.ImageOrVideoPaths.BulkInsertAsync(ImageOrVideoPaths);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }
    }
}
