using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Video_Configurations
{
    public class ConfigurationOFChatVideoServices : IFileConfigurationServices<ConfigurationOFChatVideoServices>
    {
        private readonly IConfiguration Configuration;
        public ConfigurationOFChatVideoServices(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Chats:Videos:AllowedExtension").Get<string[]>()!;
        }

        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Chats:Videos:DirectoryThatStoreFileIn").Get<string>()!;
        }

        public long MaxSize()
        {
            return Configuration.GetSection("Chats:Videos:MaxSize").Get<long>();
        }
    }
}
