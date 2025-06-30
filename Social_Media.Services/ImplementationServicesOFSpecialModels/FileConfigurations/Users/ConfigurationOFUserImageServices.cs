using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace ConstantStatementInAllProject.Files.Users
{
    public class ConfigurationOFUserImageServices : IFileConfigurationServices<ConfigurationOFUserImageServices>
    {
        IConfiguration Configuration;
        public ConfigurationOFUserImageServices(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Users:ImageOFProfile:DirectoryThatStoreFileIn").Get<string>()!;
        }
        public long MaxSize()
        {
            return Configuration.GetSection("Users:ImageOFProfile:MaxSize").Get<long>();
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Users:ImageOFProfile:AllowedExtension").Get<string[]>()!;
        }
    }
}
