using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Chat : BaseEntity
{
    public ICollection<ObjectId> UserIds { get; set; } = new List<ObjectId>();
}