using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Folks.IdentityService.Infrastructure.EntityConfigurations;

public class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        { 
            RoleId = "4dbbbc46-38fc-42f5-aabf-a8625b13a884",
            UserId = "a6beb063-dfc9-4cfd-82d2-97dc027fe34d"
        });
    }
}
