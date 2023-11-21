using MongoDB.Bson;

namespace Folks.ChatService.Application.Features.Chats.Queries.GetChatsQuery;

public record class ChatDto
{
    public ObjectId Id { get; init; }

    public IEnumerable<string> UserIds { get; init; } = new HashSet<string>();
}