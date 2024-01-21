using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

namespace Folks.ChannelsService.Application.Common.Models;

public record class ChannelMustExistCustomValidatorProperty
{
    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }
}
