using MediatR;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

public record class CreateGroupCommand : IRequest<ChannelDto>
{
    public required string Title { get; init; }

    public required IEnumerable<string> UserIds { get; init; }
}
