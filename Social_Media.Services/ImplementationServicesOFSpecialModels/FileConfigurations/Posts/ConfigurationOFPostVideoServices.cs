using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace ConstantStatementInAllProject.Files
{
    public class ConfigurationOFPostVideoServices : IFileConfigurationServices<ConfigurationOFPostVideoServices>
    {
        IConfiguration Configuration;
        public ConfigurationOFPostVideoServices(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Posts:Videos:DirectoryThatStoreFileIn").Get<string>()!;
        }
        public long MaxSize()
        {
            return Configuration.GetSection("Posts:Videos:MaxSize").Get<long>();
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Posts:Videos:AllowedExtension").Get<string[]>()!;
        }
    }
}
