using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Api.Common.Models;

public record class SendMessageRequest
{
    public required string OwnerId { get; init; }

    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string Content { get; init; }

    public required DateTimeOffset SentAt { get; init; }
}
