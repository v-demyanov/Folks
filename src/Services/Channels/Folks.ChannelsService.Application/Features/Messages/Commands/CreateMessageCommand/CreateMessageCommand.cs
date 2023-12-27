using MediatR;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;

public record class CreateMessageCommand : IRequest<MessageDto>
{
    public required string OwnerId { get; init; }

    public required string ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string Content { get; init; }

    public required DateTimeOffset SentAt { get; init; }

    public bool IsSpecific { get; init; }
}