using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications.AddPostNotification;

namespace Social_Media.InfraStructure.Configurations.Notifications.PostNotificationConfigurations
{
    public class PostNotificationConfiguration : IEntityTypeConfiguration<PostNotification>
    {
        public void Configure(EntityTypeBuilder<PostNotification> builder)
        {
            builder.HasOne(N => N.Notification).WithMany(U => U.PostNotifications).HasForeignKey(x => x.NotificationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
