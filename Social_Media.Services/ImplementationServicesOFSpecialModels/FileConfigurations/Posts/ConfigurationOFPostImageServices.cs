using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace ConstantStatementInAllProject.Files.Posts
{
    public class ConfigurationOFPostImageServices : IFileConfigurationServices<ConfigurationOFPostImageServices>
    {
        IConfiguration Configuration;
        public ConfigurationOFPostImageServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Posts:Images:DirectoryThatStoreFileIn").Get<string>()!;
        }
        public long MaxSize()
        {
            return Configuration.GetSection("Posts:Images:MaxSize").Get<long>();
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Posts:Images:AllowedExtension").Get<string[]>()!;
        }
    }
}
