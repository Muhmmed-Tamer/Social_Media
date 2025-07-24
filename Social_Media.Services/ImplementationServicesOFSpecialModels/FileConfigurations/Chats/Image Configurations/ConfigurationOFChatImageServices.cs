using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Image_Configurations
{
    public class ConfigurationOFChatImageServices : IFileConfigurationServices<ConfigurationOFChatImageServices>
    {
        private readonly IConfiguration Configuration;
        public ConfigurationOFChatImageServices(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Chats:Images:AllowedExtension").Get<string[]>()!;
        }

        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Chats:Images:DirectoryThatStoreFileIn").Get<string>()!;
        }

        public long MaxSize()
        {
            return Configuration.GetSection("Chats:Images:MaxSize").Get<long>();
        }
    }
}
