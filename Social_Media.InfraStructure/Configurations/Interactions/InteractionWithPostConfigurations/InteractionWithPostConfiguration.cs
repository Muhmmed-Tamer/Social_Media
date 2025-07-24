using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.InfraStructure.Configurations.Interactions.InteractionWithPostConfigurations
{
    public class InteractionWithPostConfiguration : IEntityTypeConfiguration<InteractionWithPost>
    {
        public void Configure(EntityTypeBuilder<InteractionWithPost> builder)
        {
            builder.HasOne(I => I.User).WithMany(U => U.InteractionsWithPost).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
