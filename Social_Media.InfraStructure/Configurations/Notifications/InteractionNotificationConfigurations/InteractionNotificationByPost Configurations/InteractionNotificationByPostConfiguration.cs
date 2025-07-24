using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.InteractionNotificationConfigurations.InteractionNotificationByPost_Configurations
{
    public class InteractionNotificationByPostConfiguration : IEntityTypeConfiguration<InteractionNotificationByPost>
    {
        public void Configure(EntityTypeBuilder<InteractionNotificationByPost> builder)
        {
            builder.HasOne(I => I.Post).WithMany(P => P.InteractionNotificationByPosts).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(IC => IC.Notification).WithMany(N => N.InteractionNotificationByPosts)
                .HasForeignKey(N => N.NotificationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
