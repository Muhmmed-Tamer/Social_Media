using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Friend_Request_Notifications.Commands.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;

namespace Social_Media.Core.Features.Friend_Request_Notifications.Commands.Handlers
{
    public class FriendRequestCommandHandler : ResponseHandler, IRequestHandler<AddFriendRequestCommand, Response<string>>,
        IRequestHandler<ConfirmFriendRequestCommand, Response<string>>,
        IRequestHandler<RejectFriendRequestCommand, Response<string>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public FriendRequestCommandHandler(IUnitOFWork UnitOFWork, ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<string>> Handle(AddFriendRequestCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.BeginTransaction())
            {
                try
                {
                    //Check If This Friend Request Is Send Before
                    FriendRequest FriendRequestNotificationIsAddedBefore = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(request.UserId_ThatReceiveFriendRequest, request.UserId_ThatSentFriendRequest);
                    if (FriendRequestNotificationIsAddedBefore is null)
                    {
                        FriendRequest Mapped_FriendRequest = UnitOFWork.Mapper.Map<FriendRequest>(request);
                        await UnitOFWork.FriendUnitOFWork.FriendRequestServices.AddAsync(Mapped_FriendRequest);
                        await UnitOFWork.FriendUnitOFWork.FriendRequestServices.SaveChangesAsync();

                        //Insert In DataBase New Notification
                        Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                        await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                        await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();
                        await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.AddAsync(new SendFriendRequestNotification()
                        {
                            FriendRequestId = Mapped_FriendRequest.Id,
                            NotificationId = Mapped_Notification.Id,
                        });

                        await UnitOFWork.NotificationUnitOFWork.SendFriendRequestNotificationService.SaveChangesAsync();

                        await UnitOFWork.FriendUnitOFWork.FriendRequestServices.CommitTransaction(Transaction);
                        //Notify Other Friend About New Friend Request
                        GetNotificationOFUser Mapped_NotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Mapped_Notification);
                        await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyUserAboutSendFriendRequestFromAnotherUser(request.UserId_ThatReceiveFriendRequest, Mapped_NotificationOFUser);
                    }
                    else
                    {
                        if (FriendRequestNotificationIsAddedBefore.status == Data.Enums.Status.Accepted)
                        {
                            return BadRequest<string>("Both User Is Friend");
                        }
                        else if (FriendRequestNotificationIsAddedBefore.status == Data.Enums.Status.Rejected)
                        {
                            //You Can Delete and Add Another Request
                        }
                        else if (FriendRequestNotificationIsAddedBefore.status == Data.Enums.Status.Pending)
                        {
                            return BadRequest<string>("The Friend Request Is Sent and Pending");
                        }
                    }
                    // Notify the user about the new friend request notification
                    return Created<string>("Friend Request Notification Added Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in Handle method of FriendRequestNotificationCommandHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(ConfirmFriendRequestCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.BeginTransaction())
            {
                try
                {
                    FriendRequest FriendRequestNotificationThatWantToConfirm = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(request.FriendUserIdThatConfirmFriendRequest, request.UserIdThatSentFriendRequest);
                    if (FriendRequestNotificationThatWantToConfirm is null)
                    {
                        return BadRequest<string>("Friend Request Notification not found.");
                    }
                    FriendRequestNotificationThatWantToConfirm.status = Data.Enums.Status.Accepted;
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.UpdateAsync(FriendRequestNotificationThatWantToConfirm);
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.SaveChangesAsync();

                    List<Friend> FriendsThatWantToAdd = new()
                    {
                        new()
                        {
                        UserId = request.FriendUserIdThatConfirmFriendRequest,
                        FriendUserId = request.UserIdThatSentFriendRequest
                        }
                    };
                    Friend Mapped_Friend1 = UnitOFWork.Mapper.Map<Friend>(request);
                    FriendsThatWantToAdd.Add(Mapped_Friend1);
                    await UnitOFWork.FriendUnitOFWork.FriendServices.BulkInsertAsync(FriendsThatWantToAdd);

                    //Insert In DataBase New Notification
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();
                    await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.AddAsync(new ConfirmFriendRequestNotification()
                    {
                        FriendRequestId = FriendRequestNotificationThatWantToConfirm.Id,
                        NotificationId = Mapped_Notification.Id,
                    });
                    await UnitOFWork.NotificationUnitOFWork.ConfirmFriendRequestNotificationService.SaveChangesAsync();
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.CommitTransaction(Transaction);
                    //Notify Friend To Add Friend
                    GetNotificationOFUser Mapped_NotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Mapped_Notification);
                    await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyUserAboutConfirmFriendRequestFromAnotherUser(request.UserIdThatSentFriendRequest, Mapped_NotificationOFUser);
                    return OK<string>("Friend Request Notification Confirmed Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in Handle method of ConfirmFriendRequestCommandHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(RejectFriendRequestCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.BeginTransaction())
            {
                try
                {
                    FriendRequest FriendRequestNotificationThatWantToConfirm = await UnitOFWork.FriendUnitOFWork.FriendRequestServices.GetFriendRequestNotificationByUserIdAndFriendUserIdAsync(request.FriendUserIdThatConfirmFriendRequest, request.UserIdThatSentFriendRequest);
                    if (FriendRequestNotificationThatWantToConfirm is null)
                    {
                        return NotFound<string>("Friend Request Notification not found.");
                    }
                    FriendRequestNotificationThatWantToConfirm.status = Data.Enums.Status.Rejected;
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.UpdateAsync(FriendRequestNotificationThatWantToConfirm);
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.SaveChangesAsync();
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.CommitTransaction(Transaction);
                    return OK<string>("Friend Request Notification Rejected Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.FriendUnitOFWork.FriendRequestServices.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in Handle method of RejectedFriendRequestCommandHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }
    }
}
