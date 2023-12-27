using Folks.ChannelsService.Application.Features.Users.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Common.Dto;

public record class MessageDto
{
    public required string Id { get; init; }

    public required string Content { get; init; }

    public DateTimeOffset SentAt { get; init; }

    public required string ChannelId { get; init; }

    public required UserDto Owner { get; init; }

    public bool IsSpecific { get; init; }
}
