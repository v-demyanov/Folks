using MongoDB.Bson;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public class Chat : BaseEntity
{
    public ICollection<string> UserIds { get; set; } = new HashSet<string>();
}