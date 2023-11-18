using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }

    public DateTimeOffset SentAt { get; set; }

    public DateTimeOffset? DeliveredAt { get; set; }

    public DateTimeOffset? SeenAt { get; set; }

    public required string OwnerId { get; set; }

    public ObjectId? GroupId { get; set; }

    public ObjectId? ChatId { get; set; }
}