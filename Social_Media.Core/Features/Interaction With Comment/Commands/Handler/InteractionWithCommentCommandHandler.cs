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
    public class InteractionWithCommentCommandHandler : ResponseHandler, IRequestHandler<AddInteractionWithCommentCommand, Response<string>>
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
            using (var Transaction = await UnitOFWork.InteractionWithCommentServices.BeginTransaction())
            {
                try
                {
                    bool CommentIsExistInCommentsOfPost = await UnitOFWork.CommentServices.CommentIsExistInAllCommentsOfPost(await UnitOFWork.CommentServices.GetCommentsByPostIdAsync(request.PostId), request.CommentId);

                    if (!CommentIsExistInCommentsOfPost)
                    {
                        await UnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                        return BadRequest<string>("Comment Not Found");
                    }

                    // Check if the user has already interacted with the comment
                    bool ExistingInteraction = await UnitOFWork.InteractionWithCommentServices.UserIsInteractWithCommentBeforeAsync(request.UserId, request.CommentId, request.PostId);
                    if (ExistingInteraction)
                    {
                        await UnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                        return BadRequest<string>("User Is Interact With Comment Before");
                    }
                    //Mapping
                    InteractionWithComment Mapped_InteractionWithComment = UnitOFWork.Mapper.Map<InteractionWithComment>(request);
                    //Add InteractionWithComment In DataBase
                    await UnitOFWork.InteractionWithCommentServices.AddAsync(Mapped_InteractionWithComment);
                    await UnitOFWork.InteractionWithCommentServices.SaveChangesAsync();

                    #region Add Notifacytion & InteractionNotificationByComment
                    string UserIdThatOwnedComment = await UnitOFWork.UserServices.GetUserIdThatOwnedComment(request.CommentId);
                    Notification Mapped_Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    Mapped_Notification.UserIdWhoReceivedTheNotification = UserIdThatOwnedComment;

                    await UnitOFWork.NotificationServices.AddAsync(Mapped_Notification);
                    await UnitOFWork.NotificationServices.SaveChangesAsync();

                    InteractionNotificationByComment Mapped_InteractionNotificationByComment = new()
                    {
                        NotificationId = Mapped_Notification.Id,
                        CommentId = request.CommentId
                    };
                    await UnitOFWork.InteractionNotificationByCommentServices.AddAsync(Mapped_InteractionNotificationByComment);
                    await UnitOFWork.InteractionNotificationByCommentServices.SaveChangesAsync();
                    #endregion
                    await UnitOFWork.InteractionWithCommentServices.CommitTransaction(Transaction);
                    // Notify InteractionWithComment In Notification Table
                    GetNotificationOFUser Mapped_NotificationOFUser = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Mapped_Notification);
                    await UnitOFWork.InteractionWithCommentHubService.NotifyOwnerOFCommentAboutInteraction(UserIdThatOwnedComment, Mapped_NotificationOFUser);

                    return Created<string>("Interaction With Comment Is Created Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.InteractionWithCommentServices.RollbackTransaction(Transaction);
                    Logger.Error(ex, "Error in InteractionWithCommentHandler");
                    return BadRequest<string>(ex.Message);
                }
            }
        }
    }
}
