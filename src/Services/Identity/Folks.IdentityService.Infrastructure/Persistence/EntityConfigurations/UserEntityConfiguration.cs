using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Infrastructure.Persistence.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user = new User()
        {
            Id = "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            LockoutEnabled = false,
            PhoneNumber = "1234567890",
        };

        var passwordHasher = new PasswordHasher<User>();
        var hash = passwordHasher.HashPassword(user, "admin123456");

        user.PasswordHash = hash;

        builder.HasData(user);
    }
}
