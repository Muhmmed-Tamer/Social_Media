using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Friends.Queries.Models;
using Social_Media.Core.Response_Structure;

namespace Social_Media.Core.Features.Friends.Queries.Handlers
{
    public class FriendsQueryHandler : ResponseHandler, IRequestHandler<GetFriendsOFUserQuery, Response<List<string>>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly Serilog.ILogger Logger;

        public FriendsQueryHandler(IUnitOFWork unitOFWork, ILogger logger)
        {
            UnitOFWork = unitOFWork;
            Logger = logger;
        }

        public async Task<Response<List<string>>> Handle(GetFriendsOFUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> FriendsUserId = await UnitOFWork.FriendUnitOFWork.FriendServices.GetFriendsIdOFUserByUserIdAsync(request.UserId);
                return OK(FriendsUserId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error while getting friends of user");
                return BadRequest<List<string>>(ex.Message);
            }
        }
    }
}
