using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Friends.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Friends;

namespace Social_Media.Core.Features.Friends.Commands.Handlers
{
    public class FriendCommandHandler : ResponseHandler, IRequestHandler<AddFriendCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public FriendCommandHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.FriendServices.BeginTransaction())
            {
                try
                {
                    Friend Mapped_Friend = UnitOFWork.Mapper.Map<Friend>(request);
                    // Check if the friend request already accepted
                    await UnitOFWork.FriendServices.AddAsync(Mapped_Friend);
                    await UnitOFWork.FriendServices.SaveChangesAsync();
                    await UnitOFWork.FriendServices.CommitTransaction(Transaction);
                    // Notify the user about the new friend addition
                    return Created<string>("Friend Added successfully.");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.FriendServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, "Error in Handle method of FriendRequestHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }
    }
}
