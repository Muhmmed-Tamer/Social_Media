using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Media.InfraStructure.Configurations.Post
{
    public class PostConfiguration : IEntityTypeConfiguration<Models.Post>
    {
        public void Configure(EntityTypeBuilder<Models.Post> builder)
        {
            builder.HasOne(U => U.User).WithMany(P => P.Posts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
