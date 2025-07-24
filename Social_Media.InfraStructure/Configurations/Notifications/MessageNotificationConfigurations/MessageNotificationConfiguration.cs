using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.MessageNotificationConfigurations
{
    public class MessageNotificationConfiguration : IEntityTypeConfiguration<MessageNotification>
    {
        public void Configure(EntityTypeBuilder<MessageNotification> builder)
        {
            builder.HasOne(N => N.Notification).WithMany(U => U.MessageNotifications).HasForeignKey(x => x.NotificationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
