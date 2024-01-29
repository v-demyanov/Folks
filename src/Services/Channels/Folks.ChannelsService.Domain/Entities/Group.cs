// Copyright (c) v-demyanov. All rights reserved.

using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Entities;

public class Group : Channel
{
    required public string Title { get; set; }

    public ObjectId OwnerId { get; set; }
}