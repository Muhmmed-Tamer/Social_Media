using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Media.InfraStructure.Configurations.Post.TextPostConfigurations
{
    public class TextPostConfiguration : IEntityTypeConfiguration<Models.TextPost>
    {
        public void Configure(EntityTypeBuilder<Models.TextPost> builder)
        {
            builder.HasOne(U => U.User).WithMany(P => P.TextPosts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
