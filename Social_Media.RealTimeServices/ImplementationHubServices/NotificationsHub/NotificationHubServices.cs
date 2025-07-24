using Microsoft.AspNetCore.SignalR;
using Serilog;
using Social_Media.Data.Models;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.RealTimeServices.Hub_Negotiation;

namespace Social_Media.RealTimeServices.ImplementationHubServices.NotificationsHub
{
    public class NotificationHubServices
    {
        private readonly IHubContext<HubNegotiation> NotificationHubContext;
        private readonly IUserConnectionRepository UserConnectionRepository;
        private readonly ILogger Logger;
        public NotificationHubServices(ILogger Logger, IUserConnectionRepository UserConnectionRepository, IHubContext<HubNegotiation> NotificationHubContext)
        {
            this.NotificationHubContext = NotificationHubContext;
            this.UserConnectionRepository = UserConnectionRepository;
            this.Logger = Logger;
        }
        public async Task NotifyOwnerOFPostAboutComment(string UserId, object Data)
        {
            try
            {
                List<UserConnection> UserConnectionOFUserThatOwnedPost = await UserConnectionRepository.GetByUserId(UserId);
                if (UserConnectionOFUserThatOwnedPost is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (NotificationHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in AddComment_Immediately in TextPostHubService");
                    return;
                }
                await NotificationHubContext.Clients.Clients(UserConnectionOFUserThatOwnedPost.Select(UC => UC.ConnectionId)).SendAsync("ReceiveNewCommentInPost", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in NotifyFriendsAboutComment in CommentHubServices");
                throw;
            }
        }
        public async Task NotifyFriendsAboutNewPost(string UserId, object Data)
        {
            try
            {
                List<string> ConnectionIdOFUsersThatIsConnectedNow = await UserConnectionRepository.GetConnectionIdOFFriends(UserId);
                if (ConnectionIdOFUsersThatIsConnectedNow is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (NotificationHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in AddPostNotification_Immediately in TextPostHubService");
                    return;
                }
                await NotificationHubContext.Clients.Clients(ConnectionIdOFUsersThatIsConnectedNow).SendAsync("ReceiveNewPostNotification", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in NotifyFriendsAboutNewPost in CommentHubServices");
                throw;
            }
        }
        public async Task NotifyUserAboutSendFriendRequestFromAnotherUser(string UserId, object Data)
        {
            try
            {
                //Get User That Receive Notification Is Online Or Not
                List<UserConnection> UserConnectionOFUserThatReceiveNewFriendRequestNotification = await UserConnectionRepository.GetByUserId(UserId);
                if (UserConnectionOFUserThatReceiveNewFriendRequestNotification is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (NotificationHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in Send_FriendRequestNotification in TextPostHubService");
                    return;
                }
                await NotificationHubContext.Clients.Clients(UserConnectionOFUserThatReceiveNewFriendRequestNotification.Select(UC => UC.ConnectionId)).SendAsync("ReceiveNewFriendRequest", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task NotifyUserAboutConfirmFriendRequestFromAnotherUser(string UserId, object Data)
        {
            try
            {
                //Get User That Receive Notification Is Online Or Not
                List<UserConnection> UserConnectionOFUserThatReceiveNewFriendRequestNotification = await UserConnectionRepository.GetByUserId(UserId);
                if (UserConnectionOFUserThatReceiveNewFriendRequestNotification is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (NotificationHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in Send_FriendRequestNotification in TextPostHubService");
                    return;
                }
                await NotificationHubContext.Clients.Clients(UserConnectionOFUserThatReceiveNewFriendRequestNotification.Select(UC => UC.ConnectionId)).SendAsync("ReceiveConfirmFriendRequest", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task NotifyUserAboutNewMessage(string UserId, object Data)
        {
            try
            {
                //Get User That Receive Notification Is Online Or Not
                List<UserConnection> UserConnectionOFUserThatReceiveNewFriendRequestNotification = await UserConnectionRepository.GetByUserId(UserId);
                if (UserConnectionOFUserThatReceiveNewFriendRequestNotification is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (NotificationHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in Send_FriendRequestNotification in TextPostHubService");
                    return;
                }
                await NotificationHubContext.Clients.Clients(UserConnectionOFUserThatReceiveNewFriendRequestNotification.Select(UC => UC.ConnectionId)).SendAsync("ReceiveNewMessageNotification", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }
    }
}
