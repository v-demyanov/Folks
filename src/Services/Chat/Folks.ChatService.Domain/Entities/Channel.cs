using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public abstract class Channel : BaseEntity
{
    public required ICollection<ObjectId> UserIds { get; set; }
}
