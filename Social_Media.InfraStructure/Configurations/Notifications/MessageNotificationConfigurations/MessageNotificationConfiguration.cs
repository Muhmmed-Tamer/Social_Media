using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.MessageNotificationConfigurations
{
    public class MessageNotificationConfiguration : IEntityTypeConfiguration<MessageNotification>
    {
        public void Configure(EntityTypeBuilder<MessageNotification> builder)
        {
            builder.HasOne(N => N.User).WithMany(U => U.MessageNotifications).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
