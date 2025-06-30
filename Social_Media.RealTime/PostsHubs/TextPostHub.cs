using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Social_Media.Core.Abstracts_UnitOFWork;

namespace Social_Media.RealTime.PostsHubs
{
    public class TextPostHub : Hub
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger<TextPostHub> Logger;
        public TextPostHub(IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
        }
        public override Task OnConnectedAsync()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in OnConnectedAsync of TextPostHub");
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error in OnDisconnectedAsync of TextPostHub");
            }
        }
    }
}
