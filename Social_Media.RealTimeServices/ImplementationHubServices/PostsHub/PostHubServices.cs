using Microsoft.AspNetCore.SignalR;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.RealTimeServices.Hub_Negotiation;

namespace Social_Media.RealTimeServices.ImplementationHubServices.PostsHub
{
    public class PostHubServices
    {
        private readonly IUserConnectionRepository UserConnectionRepository;
        private readonly IHubContext<HubNegotiation> PostHubContext;
        private readonly Serilog.ILogger Logger;

        public PostHubServices(Serilog.ILogger Logger, IUserConnectionRepository UserConnectionRepository, IHubContext<HubNegotiation> PostHubContext)
        {
            this.UserConnectionRepository = UserConnectionRepository;
            this.PostHubContext = PostHubContext;
            this.Logger = Logger;
        }

        public async Task NotifyFriendsAboutPost(string UserId, object Data)
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
                if (PostHubContext.Clients is null)
                {
                    Logger.Warning("Clients is null in AddPost_Immediately in TextPostHubService");
                    return;
                }
                await PostHubContext.Clients.Clients(ConnectionIdOFUsersThatIsConnectedNow).SendAsync("ReceivePost", Data);

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error in AddPost_Immediately in TextPostHubService");
            }
        }
    }
}
