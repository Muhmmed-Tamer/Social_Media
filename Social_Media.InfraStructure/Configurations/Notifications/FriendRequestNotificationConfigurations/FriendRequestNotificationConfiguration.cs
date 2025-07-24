using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Friends;

namespace Social_Media.InfraStructure.Configurations.Notifications.FriendRequestNotificationConfigurations
{
    public class FriendRequestNotificationConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.HasOne(N => N.User).WithMany(U => U.SendFriendRequests).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(N => N.FriendUser).WithMany(U => U.ReceiveFriendRequests).HasForeignKey(x => x.FriendUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
