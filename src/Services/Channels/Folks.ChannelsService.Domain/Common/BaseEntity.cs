using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Common;

public abstract class BaseEntity
{
    public ObjectId Id { get; set; }
}
