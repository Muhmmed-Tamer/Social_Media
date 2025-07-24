using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using Serilog;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Posts;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;
using Social_Media.Services.AbstractsServices.PostsServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;

namespace Social_Media.Services.ImplementationServices.PostServices
{
    public class PostServices : Services<Post>, IPostServices
    {
        private readonly IFileConfigurationServices<ConfigurationOFPostImageServices> PostImageConfigurationServices;
        private readonly IFileConfigurationServices<ConfigurationOFPostVideoServices> PostVideoConfigurationServices;
        private readonly IProtocolAndHostServices ProtocolAndHostServices;
        private readonly IFileService FileServices;
        private IPostRepository PostRepository;
        private readonly ILogger Logger;

        //Events
        public PostServices(ILogger Logger, IRepository<Post> Repository, IPostRepository PostRepository, IProtocolAndHostServices ProtocolAndHostServices, IFileService FileServices
            , IFileConfigurationServices<ConfigurationOFPostImageServices> PostImageConfigurationServices, IFileConfigurationServices<ConfigurationOFPostVideoServices> PostVideoConfigurationServices
            ) : base(Logger, Repository)
        {
            this.PostImageConfigurationServices = PostImageConfigurationServices;
            this.PostVideoConfigurationServices = PostVideoConfigurationServices;
            this.ProtocolAndHostServices = ProtocolAndHostServices;
            this.PostRepository = PostRepository;
            this.FileServices = FileServices;
            this.Logger = Logger;
        }

        public async Task<List<Post>> GetPostsOFUserAsync(string UserId)
        {
            try
            {
                List<Post> PostsOFUser = await PostRepository.GetPostsOFUserAsync(UserId);
                foreach (var Post in PostsOFUser)
                {
                    foreach (var ImageOrVideoPath in Post?.ImageOrVideo_Paths)
                    {
                        string Extension = Path.GetExtension(ImageOrVideoPath.Image_Or_VideoPath);
                        ExtensionType CurrentFileExtensionType = await FileServices.GetExtensionTypeOFFile(Extension, PostImageConfigurationServices.AllowedExtension(), PostVideoConfigurationServices.AllowedExtension());
                        if (CurrentFileExtensionType == ExtensionType.Image)
                        {
                            ImageOrVideoPath.Image_Or_VideoPath = ProtocolAndHostServices.GetFullPathOFFile(BaseDirectory: PostImageConfigurationServices.DirectoryThatStoreFileIn(), FileName: ImageOrVideoPath.Image_Or_VideoPath);
                        }
                        else if (CurrentFileExtensionType == ExtensionType.Video)
                        {
                            ImageOrVideoPath.Image_Or_VideoPath = ProtocolAndHostServices.GetFullPathOFFile(BaseDirectory: PostVideoConfigurationServices.DirectoryThatStoreFileIn(), FileName: ImageOrVideoPath.Image_Or_VideoPath);
                        }
                    }
                }
                return PostsOFUser;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting posts of user {UserId}", UserId);
                throw;
            }
        }

        public async Task<Post> GetPostWithImageOrVideoPathsById(int PostId)
        {
            try
            {
                return await PostRepository.GetPostWithImageOrVideoPathsById(PostId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting post with ID {PostId}", PostId);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
