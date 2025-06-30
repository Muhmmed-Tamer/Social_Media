using ConstantStatementInAllProject.Files;
using ConstantStatementInAllProject.Files.Posts;
using ConstantStatementInAllProject.Files.Users;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Image_Configurations;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Video_Configurations;

namespace Social_Media.Core.Implementation_UnitOFWork
{
    public class ConfigurationOFFilesUnitOFWork : IConfigurationOFFilesUnitOFWork
    {
        public ConfigurationOFFilesUnitOFWork(IFileConfigurationServices<ConfigurationOFChatAudioServices> ConfigurationOFChatAudioServices, IFileConfigurationServices<ConfigurationOFChatVideoServices> ConfigurationOFChatVideoServices, IFileConfigurationServices<ConfigurationOFChatImageServices> ConfigurationOFChatImageServices, IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices, IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices, IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices, IFileService FileServices)
        {
            this.ConfigurationOFChatAudioServices = ConfigurationOFChatAudioServices;
            this.ConfigurationOFChatVideoServices = ConfigurationOFChatVideoServices;
            this.ConfigurationOFChatImageServices = ConfigurationOFChatImageServices;
            this.ConfigurationOFPostImageServices = ConfigurationOFPostImageServices;
            this.ConfigurationOFPostVideoServices = ConfigurationOFPostVideoServices;
            this.ConfigurationOFUserImageServices = ConfigurationOFUserImageServices;
            this.FileServices = FileServices;
        }

        public IFileConfigurationServices<ConfigurationOFChatAudioServices> ConfigurationOFChatAudioServices { get; }

        public IFileConfigurationServices<ConfigurationOFChatVideoServices> ConfigurationOFChatVideoServices { get; }

        public IFileConfigurationServices<ConfigurationOFChatImageServices> ConfigurationOFChatImageServices { get; }

        public IFileConfigurationServices<ConfigurationOFPostImageServices> ConfigurationOFPostImageServices { get; }

        public IFileConfigurationServices<ConfigurationOFPostVideoServices> ConfigurationOFPostVideoServices { get; }

        public IFileConfigurationServices<ConfigurationOFUserImageServices> ConfigurationOFUserImageServices { get; }

        public IFileService FileServices { get; }
    }
}
