using Microsoft.EntityFrameworkCore;

namespace Social_Media.InfraStructure.Configurations.Comment
{
    public class CommentConfiguration : IEntityTypeConfiguration<Models.Comment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Models.Comment> builder)
        {
            builder.HasOne(C => C.User).WithMany(U => U.Comments).HasForeignKey(C => C.UserId);
            builder.HasOne(C => C.TextPost).WithMany(P => P.Comments).HasForeignKey(C => C.PostId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(C => C.ImageOrVideoPost).WithMany(P => P.Comments).HasForeignKey(C => C.PostId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
