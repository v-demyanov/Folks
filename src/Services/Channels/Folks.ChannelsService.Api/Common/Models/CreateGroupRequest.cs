namespace Folks.ChannelsService.Api.Common.Models;

public record class CreateGroupRequest
{
    public required string Title { get; init; }

    public required IEnumerable<string> UserIds { get; init; }
}
