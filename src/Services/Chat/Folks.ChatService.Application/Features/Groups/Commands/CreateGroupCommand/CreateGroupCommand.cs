using MediatR;

using Folks.ChatService.Application.Features.Channels.Dto;

namespace Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;

public record class CreateGroupCommand : IRequest<ChannelDto>
{
    public required string Title { get; init; }

    public required IEnumerable<string> UserIds { get; init; }
}
