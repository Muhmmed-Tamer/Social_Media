using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.FriendRequestNotificationConfigurations
{
    public class FriendRequestNotificationConfiguration : IEntityTypeConfiguration<FriendRequestNotification>
    {
        public void Configure(EntityTypeBuilder<FriendRequestNotification> builder)
        {
            builder.HasOne(N => N.User).WithMany(U => U.FriendRequestNotifications).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
