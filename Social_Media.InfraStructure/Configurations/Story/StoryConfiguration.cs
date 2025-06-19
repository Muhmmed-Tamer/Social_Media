using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Media.InfraStructure.Configurations.Story
{
    public class StoryConfiguration : IEntityTypeConfiguration<Data.Models.Story.Story>
    {
        public void Configure(EntityTypeBuilder<Data.Models.Story.Story> builder)
        {
            builder.HasOne(S => S.User).WithMany(S => S.Stories).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
