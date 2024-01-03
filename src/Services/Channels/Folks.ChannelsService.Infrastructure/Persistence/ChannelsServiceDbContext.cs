using Microsoft.EntityFrameworkCore;

using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Domain.Common.Abstractions;

namespace Folks.ChannelsService.Infrastructure.Persistence;

public class ChannelsServiceDbContext : DbContext
{
    public ChannelsServiceDbContext(DbContextOptions<ChannelsServiceDbContext> options) : base(options)
    {
    }

    public DbSet<Chat> Chats => Set<Chat>();

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<Message> Messages => Set<Message>();

    public DbSet<User> Users => Set<User>();

    public override int SaveChanges()
    {
        HandleSavingAuditableEntity();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleSavingAuditableEntity();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        BsonClassMap.RegisterClassMap<List<ObjectId>>();

        modelBuilder.Entity<Chat>().ToCollection("chats");
        modelBuilder.Entity<Group>().ToCollection("groups");
        modelBuilder.Entity<Message>().ToCollection("messages");
        modelBuilder.Entity<User>().ToCollection("users");
    }

    private void HandleSavingAuditableEntity()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            var entity = entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTimeOffset.Now;
            }
        }
    }
}