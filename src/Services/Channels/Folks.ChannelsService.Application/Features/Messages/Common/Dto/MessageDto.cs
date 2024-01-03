using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Domain.Common.Enums;

namespace Folks.ChannelsService.Application.Features.Messages.Common.Dto;

public record class MessageDto
{
    public required string Id { get; init; }

    public string? Content { get; init; }

    public DateTimeOffset SentAt { get; init; }

    public required string ChannelId { get; init; }

    public required UserDto Owner { get; init; }

    public MessageType Type { get; init; }
}
