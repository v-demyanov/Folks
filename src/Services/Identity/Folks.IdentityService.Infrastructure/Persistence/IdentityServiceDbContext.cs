using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Infrastructure.Persistence;

public class IdentityServiceDbContext : IdentityDbContext<User>
{
    public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityServiceDbContext).Assembly);
    }
}