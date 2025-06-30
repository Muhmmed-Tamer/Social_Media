using Serilog;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Chat;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.ChatRepository;
using Social_Media.Services.AbstractsServices.ChatServices;
using Social_Media.Services.AbstractsServicesOFSpecialModels;
using Social_Media.Services.AbstractsServicesOFSpecialModels.FileConfigurations;
using Social_Media.Services.AbstractsServicesOFSpecialModels.ProtocolAndHosts_Services;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Image_Configurations;
using Social_Media.Services.ImplementationServicesOFSpecialModels.FileConfigurations.Chats.Video_Configurations;

namespace Social_Media.Services.ImplementationServices.ChatServices
{
    public class MessageServices : Services<Message>, IMessageServices
    {
        IFileConfigurationServices<ConfigurationOFChatImageServices> ConfigurationOFChatImageService;
        IFileConfigurationServices<ConfigurationOFChatVideoServices> ConfigurationOFChatVideoService;
        IFileConfigurationServices<ConfigurationOFChatAudioServices> ConfigurationOFChatAudioService;
        private readonly IProtocolAndHostServices ProtocolAndHostServices;
        private readonly IMessageRepository MessageRepository;
        private readonly IFileService FileServices;
        private readonly ILogger Logger;
        public MessageServices(ILogger Logger, IRepository<Message> Repository, IMessageRepository MessageRepository, IFileService FileServices
            , IFileConfigurationServices<ConfigurationOFChatAudioServices> ConfigurationOFChatAudioService, IFileConfigurationServices<ConfigurationOFChatVideoServices> ConfigurationOFChatVideoService,
            IFileConfigurationServices<ConfigurationOFChatImageServices> ConfigurationOFChatImageService, IProtocolAndHostServices ProtocolAndHostServices
            ) : base(Logger, Repository)
        {
            this.ConfigurationOFChatAudioService = ConfigurationOFChatAudioService;
            this.ConfigurationOFChatVideoService = ConfigurationOFChatVideoService;
            this.ConfigurationOFChatImageService = ConfigurationOFChatImageService;
            this.ProtocolAndHostServices = ProtocolAndHostServices;
            this.MessageRepository = MessageRepository;
            this.FileServices = FileServices;
            this.Logger = Logger;
        }

        public async Task BulkInsertAsync(List<Message> Messages)
        {
            try
            {
                await MessageRepository.BulkInsertAsync(Messages);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Message>> GetMessagesBetweenTwoUsersAsync(string SenderId, string ReceiverId)
        {
            try
            {
                List<Message> AllMessagesBetweenTwoUser = await MessageRepository.GetMessagesBetweenTwoUsersAsync(SenderId, ReceiverId);
                foreach (Message Message in AllMessagesBetweenTwoUser)
                {
                    if (Message.MessageType == MessageType.Audio)
                    {
                        Message.Content = ProtocolAndHostServices.GetFullPathOFFile(ConfigurationOFChatAudioService.DirectoryThatStoreFileIn(), FileName: Message.Content);
                    }
                    foreach (var MediaPath in Message?.MessageMediaPaths)
                    {
                        string Extension = Path.GetExtension(MediaPath.MediaPath);
                        ExtensionType CurrentFileExtensionType = await FileServices.GetExtensionTypeOFFile(Extension, ConfigurationOFChatImageService.AllowedExtension(), ConfigurationOFChatVideoService.AllowedExtension());
                        if (CurrentFileExtensionType == ExtensionType.Image)
                        {
                            MediaPath.MediaPath = ProtocolAndHostServices.GetFullPathOFFile(ConfigurationOFChatImageService.DirectoryThatStoreFileIn(), MediaPath.MediaPath);
                        }
                        else if (CurrentFileExtensionType == ExtensionType.Video)
                        {
                            MediaPath.MediaPath = ProtocolAndHostServices.GetFullPathOFFile(ConfigurationOFChatVideoService.DirectoryThatStoreFileIn(), MediaPath.MediaPath);
                        }
                    }
                }
                return AllMessagesBetweenTwoUser;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        //You Can Override Methods Here
    }
}
