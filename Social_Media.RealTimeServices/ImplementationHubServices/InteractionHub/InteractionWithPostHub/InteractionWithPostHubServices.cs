using Microsoft.AspNetCore.SignalR;
using Serilog;
using Social_Media.Data.Models;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.RealTimeServices.Hub_Negotiation;

namespace Social_Media.RealTimeServices.ImplementationHubServices.InteractionHub.InteractionWithPostHub
{
    public class InteractionWithPostHubServices
    {
        private readonly IUserConnectionRepository UserConnectionRepository;
        private readonly IHubContext<HubNegotiation> InteractionWithPostHubContext;
        private readonly ILogger Logger;
        public InteractionWithPostHubServices(ILogger Logger, IUserConnectionRepository UserConnectionRepository, IHubContext<HubNegotiation> InteractionWithPostHubContext)
        {
            this.UserConnectionRepository = UserConnectionRepository;
            this.InteractionWithPostHubContext = InteractionWithPostHubContext;
            this.Logger = Logger;
        }
        public async Task NotifyOwnerOFPostAboutInteraction(string UserId, object Data)
        {
            try
            {
                //Get User That Receive Notification Is Online Or Not
                List<UserConnection> UserConnectionOFUserThatReceiveNewInteractionWithPostNotification = await UserConnectionRepository.GetByUserId(UserId);
                if (UserConnectionOFUserThatReceiveNewInteractionWithPostNotification is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
                // Send the message to all connected clients
                if (Data is null)
                {
                    return;
                }
                if (InteractionWithPostHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in Send_FriendRequestNotification in TextPostHubService");
                    return;
                }
                await InteractionWithPostHubContext.Clients.Clients(UserConnectionOFUserThatReceiveNewInteractionWithPostNotification.Select(UC => UC.ConnectionId)).SendAsync("ReceiveNewInteractionWithPost", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in NotifyFriendsAboutComment in CommentHubServices");
                throw;
            }
        }
    }
}
