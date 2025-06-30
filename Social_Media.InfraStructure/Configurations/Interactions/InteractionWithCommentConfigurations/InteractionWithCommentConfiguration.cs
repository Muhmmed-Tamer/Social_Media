using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.InfraStructure.Configurations.Interactions.InteractionWithCommentConfigurations
{
    public class InteractionWithCommentConfiguration : IEntityTypeConfiguration<InteractionWithComment>
    {
        public void Configure(EntityTypeBuilder<InteractionWithComment> builder)
        {
            builder.HasOne(I => I.User).WithMany(U => U.InteractionsWithComment).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
 