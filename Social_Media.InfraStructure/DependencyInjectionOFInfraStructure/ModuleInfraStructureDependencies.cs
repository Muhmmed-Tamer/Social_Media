﻿using Microsoft.Extensions.DependencyInjection;
using Social_Media.Data.Models;
using Social_Media.Data.Models.Chat;
using Social_Media.Data.Models.Comments;
using Social_Media.Data.Models.Friends;
using Social_Media.Data.Models.Interactions;
using Social_Media.Data.Models.Notifications;
using Social_Media.Data.Models.Notifications.AddCommentNotification;
using Social_Media.Data.Models.Notifications.AddPostNotification;
using Social_Media.Data.Models.Notifications.FriendRequestNotifications;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;
using Social_Media.Data.Models.Posts;
using Social_Media.Data.Models.Story;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.ConnectionRepository;
using Social_Media.InfraStructure.AbstractsRepositories.INotificationsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.InteractionsRepository;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories.PostsRepository;
using Social_Media.InfraStructure.ImplementationRepositories;
using Social_Media.InfraStructure.ImplementationRepositories.InteractionsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository.AddFriendRequest_Notification;
using Social_Media.InfraStructure.ImplementationRepositories.PostsRepository;
using Social_Media.InfraStructure.ImplementationRepositories.UserConnection;
using Social_Media.Repository;

namespace Social_Media.InfraStructure.DependencyInjectionOFInfraStructure
{
    public static class ModuleInfraStructureDependencies
    {
        public static void AddInfraStructureDependencies(this IServiceCollection Services)
        {
            Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            Services.AddScoped<IRepository<PostNotification>, Repository<PostNotification>>();
            Services.AddScoped<IRepository<Message>, Repository<Message>>();
            Services.AddScoped<IRepository<UserConnection>, Repository<UserConnection>>();
            Services.AddScoped<IUserConnectionRepository, UserConnectionRepository>();
            Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            Services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            Services.AddScoped<ICommentRepository, CommentRepository>();
            Services.AddScoped<IPostRepository, PostRepository>();
            Services.AddScoped<IInteractionWithPostRepository, InteractionWithPostRepository>();
            Services.AddScoped<IInteractionWithCommentRepository, InteractionWithCommentRepository>();
            Services.AddScoped<IFriendRepository, FriendRepository>();
            Services.AddScoped<INotificationRepository, NotificationRepository>();
            Services.AddScoped<IPostNotificationRepository, PostNotificationRepository>();
            Services.AddScoped<ImageOrVideoPathRepository>();
            Services.AddScoped<IImageOrVideoPathRepository, ImageOrVideoPathRepository>();
            Services.AddScoped<IImageOrVideoPathRepository, ImageOrVideoPathRepository>();
            Services.AddScoped<ISendFriendRequestNotificationRepository, SendFriendRequestNotificationRepository>();
            Services.AddScoped<IConfirmFriendRequestNotificationRepository, ConfirmFriendRequestNotificationRepository>();

            Services.AddScoped<ICommentNotificationRepository, CommentNotificationRepository>();
            Services.AddScoped<IRepository<CommentNotification>, CommentNotificationRepository>();
            #region Friends
            Services.AddScoped<IRepository<FriendRequest>, Repository<FriendRequest>>();
            Services.AddScoped<IRepository<Friend>, Repository<Friend>>();
            #endregion
            #region Posts
            Services.AddScoped<IRepository<Post>, Repository<Post>>();
            Services.AddScoped<IRepository<ImageOrVideoPath>, Repository<ImageOrVideoPath>>();
            #endregion
            #region Story

            Services.AddScoped<IRepository<Story>, Repository<Story>>();
            Services.AddScoped<IRepository<ImageOrVideoStoryPath>, Repository<ImageOrVideoStoryPath>>();
            #endregion
            #region Notifications
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            Services.AddScoped<IRepository<MessageNotification>, Repository<MessageNotification>>();
            Services.AddScoped<IRepository<SendFriendRequestNotification>, Repository<SendFriendRequestNotification>>();
            Services.AddScoped<IRepository<ConfirmFriendRequestNotification>, Repository<ConfirmFriendRequestNotification>>();
            #endregion
            #region Interactions
            Services.AddScoped<IRepository<InteractionWithComment>, Repository<InteractionWithComment>>();
            Services.AddScoped<IRepository<InteractionWithPost>, Repository<InteractionWithPost>>();
            Services.AddScoped<IRepository<InteractionWithStory>, Repository<InteractionWithStory>>();
            #endregion
            #region Interactions Notifications
            Services.AddScoped<IRepository<InteractionNotificationByPost>, Repository<InteractionNotificationByPost>>();
            Services.AddScoped<IRepository<InteractionNotificationByComment>, Repository<InteractionNotificationByComment>>();
            Services.AddScoped<IRepository<InteractionNotificationByStory>, Repository<InteractionNotificationByStory>>();
            Services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            #endregion
        }
    }
}
