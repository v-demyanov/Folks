using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Common.Abstractions;

public abstract class BaseEntity
{
    public ObjectId Id { get; set; }
}
