using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Interaction_With_Comment.Commands.Models;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.Core.Features.Interaction_With_Comment.Commands.Handler
{
    public class InteractionWithCommentCommandHandler : ResponseHandler, IRequestHandler<AddInteractionWithCommentCommand, Response<string>>,
        IRequestHandler<DeleteInteractionWithCommentCommand, Response<string>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public InteractionWithCommentCommandHandler(IUnitOFWork UnitOFWork, ILogger Logger)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }
        public async Task<Response<string>> Handle(AddInteractionWithCommentCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.BeginTransaction())
            {
                try
                {
                    bool CommentIsExistInCommentsOfPost = await UnitOFWork.CommentUnitOFWork.CommentServices.CommentIsExistInAllCommentsOfPost(await UnitOFWork.CommentUnitOFWork.CommentServices.GetCommentsByPostIdAsync(request.PostId), request.CommentId);

                    if (!CommentIsExistInCommentsOfPost)
                    {
                        await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                        return BadRequest<string>("Comment Not Found");
                    }

                    // Check if the user has already interacted with the comment
                    bool ExistingInteraction = await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.UserIsInteractWithCommentBeforeAsync(request.UserId, request.CommentId, request.PostId);
                    if (ExistingInteraction)
                    {
                        await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                        return BadRequest<string>("User Is Interact With Comment Before");
                    }
                    //Mapping
                    InteractionWithComment Mapped_InteractionWithComment = UnitOFWork.Mapper.Map<InteractionWithComment>(request);
                    //Add InteractionWithComment In DataBase
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.AddAsync(Mapped_InteractionWithComment);
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.SaveChangesAsync();

                    #region Add Notifacytion & InteractionNotificationByComment
                    string UserIdThatOwnedComment = await UnitOFWork.IdentityUnitOFWork.UserServices.GetUserIdThatOwnedComment(request.CommentId);
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    Mapped_Notification.UserIdWhoReceivedTheNotification = UserIdThatOwnedComment;

                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    InteractionNotificationByComment Mapped_InteractionNotificationByComment = new()
                    {
                        NotificationId = Mapped_Notification.Id,
                        CommentId = request.CommentId
                    };
                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByCommentServices.AddAsync(Mapped_InteractionNotificationByComment);
                    await UnitOFWork.InteractionUnitOFWork.InteractionNotificationByCommentServices.SaveChangesAsync();
                    #endregion
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.CommitTransaction(Transaction);
                    // Notify InteractionWithComment In Notification Table
                    GetNotificationOFUser Mapped_NotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Mapped_Notification);
                    await UnitOFWork.RealTimeUnitOFWork.InteractionWithCommentHubService.NotifyOwnerOFCommentAboutInteraction(UserIdThatOwnedComment, Mapped_NotificationOFUser);

                    return Created<string>("Interaction With Comment Is Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in InteractionWithCommentHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(DeleteInteractionWithCommentCommand command, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.BeginTransaction())
            {
                try
                {
                    InteractionWithComment interactionWithComment = await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.GetByIdAsync(command.Id);
                    interactionWithComment.IsDeleted = true;
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.UpdateAsync(interactionWithComment);
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Interaction With Comment Is Deleted Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.InteractionUnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }

        }
    }
}
