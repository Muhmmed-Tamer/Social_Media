using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Social_Media.InfraStructure.Configurations.ApplicationUser
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<Social_Media.Data.Identity.ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<Data.Identity.ApplicationUser> builder)
        {
            builder.OwnsOne(P => P.Name);
        }
    }
}
