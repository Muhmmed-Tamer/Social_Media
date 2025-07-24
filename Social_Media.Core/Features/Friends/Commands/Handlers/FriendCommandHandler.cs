using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Friends.Commands.Models;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Friends;

namespace Social_Media.Core.Features.Friends.Commands.Handlers
{
    public class FriendCommandHandler : ResponseHandler,
        IRequestHandler<DeleteFriendCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public FriendCommandHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
 

        public async Task<Response<string>> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.FriendUnitOFWork.FriendServices.BeginTransaction())
            {

                try
                {
                    var Friend = await UnitOFWork.FriendUnitOFWork.FriendServices.GetByIdAsync(request.Id);
                    var MainUser = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.Main_UserId);
                    var FriendUser = await UnitOFWork.IdentityUnitOFWork.UserServices.ManagerUser.FindByIdAsync(request.Friend_UserId);

                    if (MainUser != null && Friend != null && FriendUser != null)
                    {

                        Friend.IsDeleted = true;
                        MainUser.FriendshipsInitiated.Remove(Friend);
                        FriendUser.FriendshipsInitiated.Remove(Friend);
                        await UnitOFWork.FriendUnitOFWork.FriendServices.SaveChangesAsync();
                        await Transaction.CommitAsync();


                        return OK("Deleted Successfully");
                    }
                    return BadRequest<string>("Null Reference");


                }
                catch (Exception ex)
                {
                    {
                        Logger.Error(ex.Message);
                        await UnitOFWork.FriendUnitOFWork.FriendServices.RollbackTransaction(Transaction);
                        return BadRequest<string>(ex.Message);


                    }

                }

            }
        }

       




        
    }
}
