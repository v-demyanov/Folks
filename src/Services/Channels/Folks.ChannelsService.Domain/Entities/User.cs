// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Domain.Common.Abstractions;

using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Entities;

public class User : BaseEntity
{
    required public string SourceId { get; set; }

    required public string UserName { get; set; }

    required public string Email { get; set; }

    required public ICollection<ObjectId> ChatIds { get; set; } = new List<ObjectId>();

    required public ICollection<ObjectId> GroupIds { get; set; } = new List<ObjectId>();
}