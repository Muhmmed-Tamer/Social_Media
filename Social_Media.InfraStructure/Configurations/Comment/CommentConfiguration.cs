using Microsoft.EntityFrameworkCore;

namespace Social_Media.InfraStructure.Configurations.Comment
{
    public class CommentConfiguration : IEntityTypeConfiguration<Data.Models.Comments.Comment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Data.Models.Comments.Comment> builder)
        {
            builder.HasOne(C => C.User).WithMany(U => U.Comments).HasForeignKey(C => C.UserId);
            builder.HasOne(C => C.Post).WithMany(P => P.Comments).HasForeignKey(C => C.PostId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
