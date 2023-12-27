using MongoDB.Bson;

using Folks.ChannelsService.Domain.Common;

namespace Folks.ChannelsService.Domain.Entities;

public class Message : BaseEntity
{
    public required string Content { get; set; }

    public DateTimeOffset SentAt { get; set; }

    public ObjectId OwnerId { get; set; }

    public ObjectId? GroupId { get; set; }

    public ObjectId? ChatId { get; set; }

    public bool IsSpecific { get; set; }
}