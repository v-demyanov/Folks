// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Common.Abstractions;
using Folks.ChannelsService.Domain.Common.Enums;

using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Entities;

public class Message : BaseEntity
{
    public string? Content { get; set; }

    public DateTimeOffset SentAt { get; set; }

    public ObjectId OwnerId { get; set; }

    public ObjectId? GroupId { get; set; }

    public ObjectId? ChatId { get; set; }

    public MessageType Type { get; set; }

    required public ICollection<ObjectId> ReadByIds { get; set; } = new List<ObjectId>();
}