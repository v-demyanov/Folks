using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }

    public DateTimeOffset SentAt { get; set; }

    public required ObjectId OwnerId { get; set; }

    public ObjectId? GroupId { get; set; }

    public ObjectId? ChatId { get; set; }
}