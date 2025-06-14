using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Models;

namespace Social_Media.InfraStructure.Configurations.Post.ImageOrVideoPostConfigurations
{
    public class ImageOrVideoPostConfiguration : IEntityTypeConfiguration<ImageOrVideoPost>
    {
        public void Configure(EntityTypeBuilder<ImageOrVideoPost> builder)
        {
            builder.HasOne(U => U.User).WithMany(P => P.ImageOrVideoPost).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
