using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.PostNotificationConfigurations
{
    public class PostNotificationConfiguration : IEntityTypeConfiguration<PostNotification>
    {
        public void Configure(EntityTypeBuilder<PostNotification> builder)
        {
            builder.HasOne(N => N.User).WithMany(U => U.PostNotifications).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
