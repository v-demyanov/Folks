// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Common.Abstractions;
using Folks.ChannelsService.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Folks.ChannelsService.Infrastructure.Persistence;

public class ChannelsServiceDbContext : DbContext
{
    public ChannelsServiceDbContext(DbContextOptions<ChannelsServiceDbContext> options)
        : base(options)
    {
    }

    public ChannelsServiceDbContext()
    {
    }

    public virtual DbSet<Chat> Chats => this.Set<Chat>();

    public virtual DbSet<Group> Groups => this.Set<Group>();

    public virtual DbSet<Message> Messages => this.Set<Message>();

    public virtual DbSet<User> Users => this.Set<User>();

    public override int SaveChanges()
    {
        this.HandleSavingAuditableEntity();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.HandleSavingAuditableEntity();

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
        foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
        {
            var entity = entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTimeOffset.Now;
            }
        }
    }
}