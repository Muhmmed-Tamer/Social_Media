using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social_Media.Data.Models.Interactions;

namespace Social_Media.InfraStructure.Configurations.Interactions.InteractionWithStoryConfigurations
{
    public class InteractionWithStoryConfiguration : IEntityTypeConfiguration<InteractionWithStory>
    {
        public void Configure(EntityTypeBuilder<InteractionWithStory> builder)
        {
            builder.HasOne(I => I.User).WithMany(U => U.InteractionsWithStory).HasForeignKey(I => I.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
