using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interactions_With_Post.Commands.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.Core.Features.Interactions_With_Post.Commands.Handlers
{
    public class InteractionWithPostHandler : ResponseHandler, IRequestHandler<AddInteractionWithPostCommand, Response<string>>
      , IRequestHandler<DeleteInteractionWithPostCommand, Response<string>>
    {
        private readonly ILogger Logger;
        private readonly IUnitOFWork UnitOFWork;
        public InteractionWithPostHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.Logger = Logger;
            this.UnitOFWork = UnitOFWork;
        }
        public async Task<Response<string>> Handle(AddInteractionWithPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.BeginTransaction())
            {
                try
                {
                    if (await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.TheUserIsInteractWithPostBeforeAsync(request.UserIdThatInteractWithPost, request.PostIdThatInteractWith))
                    {
                        await Transaction.RollbackAsync(cancellationToken);
                        return BadRequest<string>("User Is Interact With Post Before");
                    }

                    InteractionWithPost Mapped_InteractionWithPost = UnitOFWork.Mapper.Map<InteractionWithPost>(request);

                    await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.AddAsync(Mapped_InteractionWithPost);
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.SaveChangesAsync();

                    #region Add InteractionWithPostIn Notification Table
                    string UserIdThatOwnedPost = await UnitOFWork.IdentityUnitOFWork.UserServices.GetUserIdThatOwnedPost(request.PostIdThatInteractWith);

                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    Notification.UserIdWhoReceivedTheNotification = UserIdThatOwnedPost;


                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    Data.Models.Notifications.Interactions_Notifications.InteractionNotificationByPost Mapped_InteractionNotificationByPost =
                        UnitOFWork.Mapper.Map<Data.Models.Notifications.Interactions_Notifications.InteractionNotificationByPost>(request);
                    Mapped_InteractionNotificationByPost.NotificationId = Notification.Id;


                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByPostServices.AddAsync(Mapped_InteractionNotificationByPost);
                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByPostServices.SaveChangesAsync();
                    #endregion
                    await Transaction.CommitAsync(cancellationToken);
                    //Notify User That Create This Post  
                    GetNotificationOFUser Mapped_NotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.RealTimeUnitOFWork.InteractionWithPostHubService.NotifyOwnerOFPostAboutInteraction(UserIdThatOwnedPost, Mapped_NotificationOFUser);
                    return Created<string>("Interaction With Post Added Successfully");
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync(cancellationToken);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(DeleteInteractionWithPostCommand command, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.BeginTransaction())
            {
                try
                {
                    InteractionWithPost interactionWithPost = await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.GetByIdAsync(command.Id);
                    interactionWithPost.IsDeleted = true;
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.UpdateAsync(interactionWithPost);
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK<string>("Interaction With Post Deleted Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithPostServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);

                }
            }

        }
    }
}
