using Microsoft.EntityFrameworkCore;

namespace Social_Media.InfraStructure.Configurations.Post.PostConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Data.Models.Posts.Post>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Data.Models.Posts.Post> builder)
        {
        }
    }
}
