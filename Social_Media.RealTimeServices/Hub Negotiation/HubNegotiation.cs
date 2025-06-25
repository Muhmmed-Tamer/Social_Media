using Microsoft.AspNetCore.SignalR;
using Serilog;
using Social_Media.Data.Models;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using System.Security.Claims;

namespace Social_Media.RealTimeServices.Hub_Negotiation
{
    public class HubNegotiation : Hub
    {
        private readonly IUserConnectionRepository UserConnectionRepository;
        private readonly ILogger Logger;
        public HubNegotiation(ILogger Logger, IUserConnectionRepository UserConnectionRepository)
        {
            this.UserConnectionRepository = UserConnectionRepository;
            this.Logger = Logger;
        }
        public async override Task OnConnectedAsync()
        {
            using (var Transaction = await UserConnectionRepository.BeginTransaction())
            {
                try
                {
                    string ConnectionId = Context.ConnectionId;
                    string? UserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    // For testing purposes, you can hardcode UserId
                    UserId = "3383222c-c21e-4a17-8190-03fb39e045ca";
                    UserConnection UserConnectionIsFoundBefore = await UserConnectionRepository.GetByUserId(UserId);
                    if (string.IsNullOrEmpty(UserId))
                    {
                        await UserConnectionRepository.RollbackTransaction(Transaction);
                        Logger.Warning("UserId is null or empty in OnConnectedAsync in TextPostHubService");
                        return; // Exit if UserId is not available
                    }
                    if (UserConnectionIsFoundBefore is not null)
                    {
                        UserConnectionIsFoundBefore.ConnectionId = ConnectionId;
                        await UserConnectionRepository.UpdateAsync(UserConnectionIsFoundBefore);
                        await UserConnectionRepository.SaveChangesAsync();
                        await UserConnectionRepository.CommitTransaction(Transaction);
                    }
                    if (UserConnectionIsFoundBefore is null)
                    {
                        await UserConnectionRepository.AddAsync(new Data.Models.UserConnection()
                        {
                            ConnectionId = ConnectionId,
                            UserId = UserId
                        });
                        await UserConnectionRepository.SaveChangesAsync();
                        await UserConnectionRepository.CommitTransaction(Transaction);
                    }
                    await base.OnConnectedAsync();
                }
                catch (Exception ex)
                {
                    await UserConnectionRepository.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in OnConnectedAsync in TextPostHubService");
                }
            }
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            using (var Transaction = await UserConnectionRepository.BeginTransaction())
            {
                try
                {
                    string ConnectionId = Context.ConnectionId;
                    UserConnection UserConnectionIfFoundOrNot = await UserConnectionRepository.GetByConnectionId(ConnectionId);
                    if (UserConnectionIfFoundOrNot is not null)
                    {
                        await UserConnectionRepository.DeleteByConnectionId(ConnectionId);
                        await UserConnectionRepository.SaveChangesAsync();
                        await UserConnectionRepository.CommitTransaction(Transaction);
                    }
                    else
                    {
                        Logger.Warning("User Want To DisConnect But Is Not Found Is An Online User");
                        await UserConnectionRepository.RollbackTransaction(Transaction);
                    }
                    await base.OnDisconnectedAsync(exception);
                    return;
                }
                catch
                {
                    await UserConnectionRepository.RollbackTransaction(Transaction);
                    Logger.Error(exception, "Error in OnDisconnectedAsync in TextPostHubService");
                }
            }
        }
    }
}
