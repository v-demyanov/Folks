using MongoDB.Bson;

namespace Folks.ChatService.Application.Features.Users.Dto;

public record class UserDto
{
    public required string Id { get; set; }

    public required string SourceId { get; set; }

    public required string UserName { get; set; }

    public required string Email { get; set; }
}
