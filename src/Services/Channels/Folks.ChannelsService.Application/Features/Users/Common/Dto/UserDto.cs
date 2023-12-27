namespace Folks.ChannelsService.Application.Features.Users.Common.Dto;

public record class UserDto
{
    public required string Id { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }
}
