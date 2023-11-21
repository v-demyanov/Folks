using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Group : BaseEntity
{
    public required string Title { get; set; }

    public ICollection<ObjectId> UserIds { get; set; } = new List<ObjectId>();
}
