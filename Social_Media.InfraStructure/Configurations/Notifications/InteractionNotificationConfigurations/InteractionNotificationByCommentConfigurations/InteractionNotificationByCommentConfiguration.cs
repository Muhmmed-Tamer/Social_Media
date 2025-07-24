using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Notifications.Interactions_Notifications;

namespace Social_Media.InfraStructure.Configurations.Notifications.InteractionNotificationConfigurations.InteractionNotificationByCommentConfigurations
{
    public class InteractionNotificationByCommentConfiguration : IEntityTypeConfiguration<InteractionNotificationByComment>
    {
        public void Configure(EntityTypeBuilder<InteractionNotificationByComment> builder)
        {
            builder.HasOne(IC => IC.Comment).WithMany().OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(IC => IC.Notification).WithMany(N => N.InteractionNotificationByComments)
                .HasForeignKey(N => N.NotificationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
