using MongoDB.Bson;

using System.ComponentModel.DataAnnotations.Schema;

using Folks.ChatService.Domain.Common;

namespace Folks.ChatService.Domain.Entities;

public abstract class Channel : BaseEntity
{
    [NotMapped]
    public ICollection<User> Users { get; set; } = new List<User>();

    public required ICollection<ObjectId> UserIds { get; set; }
}
