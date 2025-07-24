using Microsoft.AspNetCore.SignalR;
using Serilog;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.RealTimeServices.Hub_Negotiation;

namespace Social_Media.RealTimeServices.ImplementationHubServices.CommentsHub
{
    public class CommentHubServices
    {
        private readonly IUserConnectionRepository UserConnectionRepository;
        private readonly IHubContext<HubNegotiation> CommentHubContext;
        private readonly ILogger Logger;
        public CommentHubServices(ILogger Logger, IUserConnectionRepository UserConnectionRepository, IHubContext<HubNegotiation> CommentHubContext)
        {
            this.UserConnectionRepository = UserConnectionRepository;
            this.CommentHubContext = CommentHubContext;
            this.Logger = Logger;
        }
        public async Task NotifyFriendsAboutComment(string UserId, object Data)
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
                if (CommentHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in AddComment_Immediately in TextPostHubService");
                    return;
                }
                await CommentHubContext.Clients.Clients(ConnectionIdOFUsersThatIsConnectedNow).SendAsync("ReceiveComment", Data);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in NotifyFriendsAboutComment in CommentHubServices");
                throw;
            }
        }

    }
}
