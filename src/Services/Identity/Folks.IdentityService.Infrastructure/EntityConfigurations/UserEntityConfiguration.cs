using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Infrastructure.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user = new User()
        {
            Id = "a6beb063-dfc9-4cfd-82d2-97dc027fe34d",
            UserName = "Admin",
            Email = "admin@gmail.com",
            LockoutEnabled = false,
            PhoneNumber = "1234567890"
        };

        var passwordHasher = new PasswordHasher<User>();
        passwordHasher.HashPassword(user, "admin123456");

        builder.HasData(user);
    }
}
