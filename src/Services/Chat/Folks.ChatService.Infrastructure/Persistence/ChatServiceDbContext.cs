using Microsoft.EntityFrameworkCore;

using MongoDB.EntityFrameworkCore.Extensions;

using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Infrastructure.Persistence;

public class ChatServiceDbContext : DbContext
{
    public ChatServiceDbContext(DbContextOptions<ChatServiceDbContext> options) : base(options)
    {
    }

    public DbSet<Chat> Chats => Set<Chat>();

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Chat>().ToCollection("chats");
        modelBuilder.Entity<Group>().ToCollection("groups");
        modelBuilder.Entity<Message>().ToCollection("messages");
    }
}