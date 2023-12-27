using MongoDB.Bson;

namespace Folks.ChannelsService.Domain.Entities;

public class Group : Channel
{
    public required string Title { get; set; }

    public ObjectId OwnerId { get; set; }
}