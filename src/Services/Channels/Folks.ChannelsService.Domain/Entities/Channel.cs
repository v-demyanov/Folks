// Copyright (c) v-demyanov. All rights reserved.

using System.ComponentModel.DataAnnotations.Schema;

using Folks.ChannelsService.Domain.Common.Abstractions;

using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Entities;

public abstract class Channel : BaseEntity, IAuditableEntity
{
    [NotMapped]
    public ICollection<User> Users { get; set; } = new List<User>();

    required public ICollection<ObjectId> UserIds { get; set; } = new List<ObjectId>();

    public DateTimeOffset CreatedAt { get; set; }
}