using MongoDB.Bson;

namespace Folks.ChatService.Domain.Common;

public abstract class BaseEntity
{
    public ObjectId Id { get; set; }
}
