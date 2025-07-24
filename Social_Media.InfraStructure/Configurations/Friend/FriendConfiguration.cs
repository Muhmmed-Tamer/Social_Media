using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Media.InfraStructure.Configurations.Friend
{
    public class FriendConfiguration : IEntityTypeConfiguration<Data.Models.Friends.Friend>
    {
        public void Configure(EntityTypeBuilder<Data.Models.Friends.Friend> builder)
        {
            builder.HasOne(F => F.MainUser).WithMany(U => U.FriendshipsInitiated).HasForeignKey(F => F.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(F => F.FriendUser).WithMany(u => u.FriendshipsReceived).HasForeignKey(f => f.FriendUserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
