using ConstantStatementInAllProject.Files;
using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Chats.Commands.Models;
using Social_Media.Core.Features.Chats.Queries.Results;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Enums;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Features.Chats.Commands.Handlers
{
    public class ChatCommandHandler : ResponseHandler, IRequestHandler<AddTextMessageCommand, Response<string>>,
        IRequestHandler<AddAudioMessageCommand, Response<string>>, IRequestHandler<AddMediaMessageCommand, Response<string>>
        , IRequestHandler<DeleteMessageCommand, Response<string>>, IRequestHandler<UpdateMessageCommand, Response<string>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;

        public ChatCommandHandler(IUnitOFWork UnitOFWork, ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }

        public async Task<Response<string>> Handle(AddTextMessageCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.ChatUnitOFWork.MessageServices.BeginTransaction())
            {
                try
                {
                    Message Mapped_TextMessage = UnitOFWork.Mapper.Map<Message>(request);
                    await UnitOFWork.ChatUnitOFWork.MessageServices.AddAsync(Mapped_TextMessage);
                    await UnitOFWork.ChatUnitOFWork.MessageServices.SaveChangesAsync();
                    //Add Notification
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    MessageNotification messageNotification = new()
                    {
                        NotificationId = Mapped_Notification.Id,
                        MessageId = Mapped_TextMessage.Id,
                    };
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.AddAsync(messageNotification);
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.SaveChangesAsync();
                    await UnitOFWork.ChatUnitOFWork.MessageServices.CommitTransaction(Transaction);
                    //Notify User About New Message
                    await RealTimeNotification(Mapped_TextMessage, Mapped_Notification, request.ReceiverId);
                    return Created<string>("Message Created Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }
        public async Task<Response<string>> Handle(AddAudioMessageCommand request, CancellationToken cancellationToken)
        {
            (string AudioPath, bool IsStoredSuccessfully) = (string.Empty, false);
            using (var Transaction = await UnitOFWork.ChatUnitOFWork.MessageServices.BeginTransaction())
            {
                try
                {
                    Message Mapped_AudioMessage = UnitOFWork.Mapper.Map<Message>(request);
                    (AudioPath, IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFile(request.Audio, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.MaxSize(),
                      UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.AllowedExtension());
                    Mapped_AudioMessage.Content = AudioPath;

                    if (AudioPath.Contains(FilesConstants.ErrorExtensionFile) || AudioPath.Contains(FilesConstants.ErrorSizeFile) && !IsStoredSuccessfully)
                    {
                        await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(AudioPath + string.Join(",", UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.AllowedExtension()));
                    }
                    else if (!IsStoredSuccessfully)
                    {
                        await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(AudioPath + string.Join(",", UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.AllowedExtension()));
                    }
                    await UnitOFWork.ChatUnitOFWork.MessageServices.AddAsync(Mapped_AudioMessage);
                    await UnitOFWork.ChatUnitOFWork.MessageServices.SaveChangesAsync();
                    //Add Notification
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    MessageNotification Message_Notification = new()
                    {
                        NotificationId = Mapped_Notification.Id,
                        MessageId = Mapped_AudioMessage.Id,
                    };
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.AddAsync(Message_Notification);
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.SaveChangesAsync();
                    await UnitOFWork.ChatUnitOFWork.MessageServices.CommitTransaction(Transaction);

                    Mapped_AudioMessage.Content = UnitOFWork.ProtocolAndHostServices.GetFullPathOFFile(UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.DirectoryThatStoreFileIn(), FileName: AudioPath);
                    await RealTimeNotification(Mapped_AudioMessage, Mapped_Notification, request.ReceiverId);
                    return Created<string>("Audio Message Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                    await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFile(AudioPath, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatAudioServices.DirectoryThatStoreFileIn());
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }
        public async Task<Response<string>> Handle(AddMediaMessageCommand request, CancellationToken cancellationToken)
        {
            (List<string> MediaPaths, bool IsStoredSuccessfully) = (new List<string>(), false);
            using (var Transaction = await UnitOFWork.ChatUnitOFWork.MessageServices.BeginTransaction())
            {
                try
                {
                    Message Mapped_MediaMessage = UnitOFWork.Mapper.Map<Message>(request);
                    (MediaPaths, IsStoredSuccessfully) = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GeneratePathOFFiles(request.Medias, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.MaxSize(),
                        UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.AllowedExtension(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.MaxSize(),
                        UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.AllowedExtension());

                    if (MediaPaths.Contains(FilesConstants.ErrorExtensionFiles) || MediaPaths.Contains(FilesConstants.ErrorSizeFiles) && !IsStoredSuccessfully)
                    {
                        await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(MediaPaths + MediaPaths.Select(E => E).ToString());
                    }
                    else if (!IsStoredSuccessfully)
                    {
                        await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(MediaPaths + MediaPaths.Select(E => E).ToString());
                    }
                    await UnitOFWork.ChatUnitOFWork.MessageServices.AddAsync(Mapped_MediaMessage);
                    await UnitOFWork.ChatUnitOFWork.MessageServices.SaveChangesAsync();

                    List<MessageMediaPath> MessageMediaPaths = new();
                    foreach (string MediaPath in MediaPaths)
                    {
                        MessageMediaPaths.Add(new MessageMediaPath()
                        {
                            MediaPath = MediaPath,
                            MessageId = Mapped_MediaMessage.Id
                        });
                    }

                    await UnitOFWork.ChatUnitOFWork.MessageMediaPathService.BulkInsertAsync(MessageMediaPaths);
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    MessageNotification Message_Notification = new()
                    {
                        NotificationId = Mapped_Notification.Id,
                        MessageId = Mapped_MediaMessage.Id,
                    };
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.AddAsync(Message_Notification);
                    await UnitOFWork.NotificationUnitOFWork.MessageNotificationServices.SaveChangesAsync();

                    await UnitOFWork.ChatUnitOFWork.MessageServices.CommitTransaction(Transaction);
                    Mapped_MediaMessage.MessageMediaPaths = MessageMediaPaths;

                    await GetFullPathOFMediaChat(MessageMediaPaths);
                    await RealTimeNotification(Mapped_MediaMessage, Mapped_Notification, request.ReceiverId);
                    return Created<string>("Media Message Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                    await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.DeleteFiles(MediaPaths, UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.DirectoryThatStoreFileIn(), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.DirectoryThatStoreFileIn());
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.ChatUnitOFWork.MessageServices.BeginTransaction())
            {
                try
                {
                    Message message = await UnitOFWork.ChatUnitOFWork.MessageServices.GetByIdAsync(request.Id);
                    message.IsDeleted = true;
                    await UnitOFWork.ChatUnitOFWork.MessageServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Deleted Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }

        }

        public async Task<Response<string>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.ChatUnitOFWork.MessageServices.BeginTransaction())
            {
                try
                {
                    Message Mapped_UpdatedMessage = await UnitOFWork.ChatUnitOFWork.MessageServices.GetByIdAsync(request.MessageId);
                    //Mapping the Updated Message
                    Mapped_UpdatedMessage.Content = request.New_Content;
                    Mapped_UpdatedMessage.CreatedAt = DateTime.UtcNow;
                    Mapped_UpdatedMessage.IsUpdated = true;

                    await UnitOFWork.ChatUnitOFWork.MessageServices.UpdateAsync(Mapped_UpdatedMessage);
                    await UnitOFWork.ChatUnitOFWork.MessageServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Updated Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.ChatUnitOFWork.MessageServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);

                }
            }

        }
        private async Task RealTimeNotification(object TypeOFMessage, Notification Notification, string ReceiverId)
        {
            try
            {
                GetMessagesBetweenTwoUsers Mapped_GetMessagesBetweenTwoUsers = UnitOFWork.Mapper.Map<GetMessagesBetweenTwoUsers>(TypeOFMessage);
                await UnitOFWork.RealTimeUnitOFWork.MessageHubService.NotifyReceiverUserAboutMessage(ReceiverId, Mapped_GetMessagesBetweenTwoUsers);
                GetNotificationOFUser Mapped_GetNotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyUserAboutNewMessage(ReceiverId, Mapped_GetNotificationOFUser);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        private async Task GetFullPathOFMediaChat(List<MessageMediaPath> MessageMediaPaths)
        {
            foreach (var MediaPath in MessageMediaPaths)
            {
                ExtensionType TypeOFMedia = await UnitOFWork.ConfigurationOfFilesUnitOFWork.FileServices.GetExtensionTypeOFFile(Path.GetExtension(MediaPath.MediaPath), UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.AllowedExtension(),
                    UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.AllowedExtension());
                if (TypeOFMedia == ExtensionType.Image)
                {
                    MediaPath.MediaPath = UnitOFWork.ProtocolAndHostServices.GetFullPathOFFile(UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatImageServices.DirectoryThatStoreFileIn(), FileName: MediaPath.MediaPath);
                }
                else if (TypeOFMedia == ExtensionType.Video)
                {
                    MediaPath.MediaPath = UnitOFWork.ProtocolAndHostServices.GetFullPathOFFile(UnitOFWork.ConfigurationOfFilesUnitOFWork.ConfigurationOFChatVideoServices.DirectoryThatStoreFileIn(), FileName: MediaPath.MediaPath);
                }
            }
        }
    }
}
