﻿using Serilog;
using Social_Media.Data.Models.Notifications;
using Social_Media.InfraStructure.AbstractsRepositories;
using Social_Media.InfraStructure.AbstractsRepositories.Notifications;
using Social_Media.Services.ImplementationServices;

namespace Social_Media.InfraStructure.ImplementationRepositories.NotificationsRepository
{
    public class MessageNotificationServices : Services<MessageNotification>, IMessageNotificationServices
    {
        public MessageNotificationServices(ILogger Logger, IRepository<MessageNotification> Repository) : base(Logger, Repository)
        {
        }
        //You Can Override Methods Here
    }
}
