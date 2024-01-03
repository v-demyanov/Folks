using MongoDB.Bson;

using System.ComponentModel.DataAnnotations.Schema;
using Folks.ChannelsService.Domain.Common.Abstractions;

namespace Folks.ChannelsService.Domain.Entities;

public abstract class Channel : BaseEntity, IAuditableEntity
{
    [NotMapped]
    public ICollection<User> Users { get; set; } = new List<User>();

    public required ICollection<ObjectId> UserIds { get; set; } = new List<ObjectId>();

    public DateTimeOffset CreatedAt { get; set; }
}