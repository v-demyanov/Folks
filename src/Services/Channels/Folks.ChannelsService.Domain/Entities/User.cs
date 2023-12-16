using MongoDB.Bson;

using Folks.ChannelsService.Domain.Common;

namespace Folks.ChannelsService.Domain.Entities;

public class User : BaseEntity
{
    public required string SourceId { get; set; }

    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required ICollection<ObjectId> ChatIds { get; set; } = new List<ObjectId>();

    public required ICollection<ObjectId> GroupIds { get; set; } = new List<ObjectId>();
}