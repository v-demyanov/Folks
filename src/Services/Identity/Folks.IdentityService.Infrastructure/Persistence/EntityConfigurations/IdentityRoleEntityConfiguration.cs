using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Folks.IdentityService.Infrastructure.Persistence.EntityConfigurations;

public class IdentityRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new IdentityRole
        {
            Id = "4dbbbc46-38fc-42f5-aabf-a8625b13a884",
            Name = "Admin",
            ConcurrencyStamp = "1",
            NormalizedName = "Admin"
        });
    }
}
