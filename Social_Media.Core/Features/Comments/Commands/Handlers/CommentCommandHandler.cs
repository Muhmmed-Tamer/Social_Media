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
    public class CommentCommandHandler : ResponseHandler, IRequestHandler<AddCommentToPostCommand, Response<string>>,
        IRequestHandler<AddReplyOFCommentCommand, Response<string>>,
        IRequestHandler<DeleteCommentCommand, Response<string>>, IRequestHandler<UpdateCommentCommand, Response<string>>

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
            using (var Transaction = await UnitOFWork.CommentUnitOFWork.CommentServices.BeginTransaction())
            {
                try
                {
                    string IdOFUserThatOwnedPost = await UnitOFWork.IdentityUnitOFWork.UserServices.GetUserIdThatOwnedPost(request.PostId);
                    Comment Mapped_Comment = UnitOFWork.Mapper.Map<Comment>(request);

                    await UnitOFWork.CommentUnitOFWork.CommentServices.AddAsync(Mapped_Comment);
                    await UnitOFWork.CommentUnitOFWork.CommentServices.SaveChangesAsync();

                    // 4. Bulk create notifications
                    Notification Notification = UnitOFWork.Mapper.Map<Notification>(request);
                    Notification.UserIdWhoReceivedTheNotification = IdOFUserThatOwnedPost;

                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.AddAsync(Notification);
                    await UnitOFWork.NotificationUnitOFWork.NotificationServices.SaveChangesAsync();

                    // 5. Bulk create post-notification relationships
                    CommentNotification CommentNotification = new()
                    {
                        CommentId = Mapped_Comment.Id,
                        NotificationId = Notification.Id
                    };

                    await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.AddAsync(CommentNotification);
                    await UnitOFWork.NotificationUnitOFWork.CommentNotificationService.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    // Notify the user about the new comment creation
                    CommentsOFPostQuery Mapped_CommentsOFPostQuery = UnitOFWork.Mapper.Map<CommentsOFPostQuery>(Mapped_Comment);
                    await UnitOFWork.RealTimeUnitOFWork.CommentHubService.NotifyFriendsAboutComment(request.UserId, Mapped_CommentsOFPostQuery);
                    GetNotificationOFUser NotificationData = UnitOFWork.Mapper.Map<GetNotificationOFUser>(Notification);
                    await UnitOFWork.RealTimeUnitOFWork.NotificationHubService.NotifyOwnerOFPostAboutComment(IdOFUserThatOwnedPost, NotificationData);

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


        public async Task<Response<string>> Handle(AddReplyOFCommentCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.CommentUnitOFWork.ReplyOFCommentService.BeginTransaction())
            {
                try
                {
                    ReplyOFComment Mapped_ReplyOFComment = UnitOFWork.Mapper.Map<ReplyOFComment>(request);
                    await UnitOFWork.CommentUnitOFWork.ReplyOFCommentService.AddAsync(Mapped_ReplyOFComment);
                    await UnitOFWork.CommentUnitOFWork.ReplyOFCommentService.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return Created<string>("Reply To Comment Is Created Successfully");
                }
                catch (Exception ex)
                {
                    await Transaction.RollbackAsync();
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.CommentUnitOFWork.CommentServices.BeginTransaction())
            {
                try
                {

                    Comment comment = await UnitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(request.Id);
                    comment.IsDeleted = true;

                    await UnitOFWork.CommentUnitOFWork.CommentServices.SaveChangesAsync();

                    await Transaction.CommitAsync();

                    return OK("Comment Is Deleted Successfully");

                }
                catch (Exception ex)
                {
                    await UnitOFWork.CommentUnitOFWork.CommentServices.RollbackTransaction(Transaction);

                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);
                }
            }
        }

        public async Task<Response<string>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            using (var Transaction = await UnitOFWork.CommentUnitOFWork.CommentServices.BeginTransaction())
            {

                try
                {
                    Comment comment = await UnitOFWork.CommentUnitOFWork.CommentServices.GetByIdAsync(request.CommentId);
                    comment.IsUpdated = true;
                    comment.CreatedAt = DateTimeOffset.Now;
                    comment.Content = request.New_Content;
                    await UnitOFWork.CommentUnitOFWork.CommentServices.UpdateAsync(comment);
                    await UnitOFWork.CommentUnitOFWork.CommentServices.SaveChangesAsync();
                    await Transaction.CommitAsync();
                    return OK("Edit Comment Successfully");
                }
                catch (Exception ex)
                {
                    await UnitOFWork.CommentUnitOFWork.CommentServices.RollbackTransaction(Transaction);
                    Logger.Error(ex.Message, ex);
                    return BadRequest<string>(ex.Message);

                }

            }
        }
    }
}
