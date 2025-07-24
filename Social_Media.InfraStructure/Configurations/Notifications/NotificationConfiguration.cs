using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasOne(N => N.UserThatCausedNotification).WithMany(U => U.UserThatCausedNotifications).HasForeignKey(F => F.UserIdWhoCausedTheNotificationToBeSent).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(N => N.UserThatReceivedNotification).WithMany(U => U.UserThatReceiveNotifications).HasForeignKey(F => F.UserIdWhoReceivedTheNotification).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
