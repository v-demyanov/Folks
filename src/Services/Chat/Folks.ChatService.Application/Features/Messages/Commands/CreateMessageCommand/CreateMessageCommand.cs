using MediatR;

using Folks.ChatService.Application.Features.Channels.Enums;
using Folks.ChatService.Application.Features.Messages.Dto;

namespace Folks.ChatService.Application.Features.Messages.Commands.CreateMessageCommand;

public record class CreateMessageCommand : IRequest<MessageDto>
{
    public required string OwnerId { get; init; }

    public string? ChannelId { get; init; }

    public ChannelType ChannelType { get; init; }

    public required string Content { get; init; }

    public required DateTimeOffset SentAt { get; init; }
}