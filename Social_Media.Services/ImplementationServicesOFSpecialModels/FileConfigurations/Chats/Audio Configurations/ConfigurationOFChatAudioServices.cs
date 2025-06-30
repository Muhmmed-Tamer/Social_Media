using Microsoft.Extensions.Configuration;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;

namespace Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats
{
    public class ConfigurationOFChatAudioServices : IFileConfigurationServices<ConfigurationOFChatAudioServices>
    {
        private readonly IConfiguration Configuration;
        public ConfigurationOFChatAudioServices(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string[] AllowedExtension()
        {
            return Configuration.GetSection("Chats:Audios:AllowedExtension").Get<string[]>()!;
        }

        public string DirectoryThatStoreFileIn()
        {
            return Configuration.GetSection("Chats:Audios:DirectoryThatStoreFileIn").Get<string>()!;
        }

        public long MaxSize()
        {
            return Configuration.GetSection("Chats:Audios:MaxSize").Get<long>();
        }
    }
}
