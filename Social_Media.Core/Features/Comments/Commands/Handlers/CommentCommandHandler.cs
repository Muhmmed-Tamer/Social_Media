using MediatR;
using Serilog;
using Social_Media.Core.Abstracts_UnitOFWork;
using Social_Media.Core.Features.Comments.Commands.Models;
using Social_Media.Core.Features.Comments.Queires.Results;
using Social_Media.Core.Features.Notifications.Queries.Results;
using Social_Media.Core.Response_Structure;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;

namespace Social_Media.Core.Features.Comments.Commands.Handlers
{
    public class CommentCommandHandler : ResponseHandler, IRequestHandler<AddCommentToPostCommand, Response<string>>
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly ILogger Logger;
        public CommentCommandHandler(ILogger Logger, IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
            this.Logger = Logger;
        }

        public async Task<Response<string>> Handle(AddCommentToPostCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.CommentServices.BeginTransaction())
            {
                try
                {
                    string IdOFUserThatOwnedPost = await UnitOFWork.UserServices.GetUserIdThatOwnedPost(request.PostId);
                    Comment Mapped_Comment = UnitOFWork.Mapper.Map<Comment>(request);

                    await UnitOFWork.CommentServices.AddAsync(Mapped_Comment);
                    await UnitOFWork.CommentServices.SaveChangesAsync();

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    Notification.UserIdWhoReceivedTheNotification = IdOFUserThatOwnedPost;

                    await UnitOFWork.NotificationServices.AddAsync(Notification);
                    await UnitOFWork.NotificationServices.SaveChangesAsync();

                    // 5. Bulk create post-notification relationships
                    CommentNotification CommentNotification = new()
                    {
                        CommentId = Mapped_Comment.Id,
                        NotificationId = Notification.Id
                    };

                    await UnitOFWork.CommentNotificationService.AddAsync(CommentNotification);
                    await UnitOFWork.CommentNotificationService.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    // Notify the user about the new comment creation
                    CommentsOFPostQuery Mapped_CommentsOFPostQuery = UnitOFWork.Mapper.Map<CommentsOFPostQuery>(Mapped_Comment);
                    await UnitOFWork.CommentHubService.NotifyFriendsAboutComment(request.UserId, Mapped_CommentsOFPostQuery);
                    GetNotificationOFUser NotificationData = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.NotificationHubService.NotifyOwnerOFPostAboutComment(IdOFUserThatOwnedPost, NotificationData);

                    return OK("Comment Is Created Successfully");
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }
    }
}
