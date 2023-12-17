using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using MediatR;

using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Channels.Enums;
using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

namespace Folks.ChannelsService.Api.Hubs;

[Authorize]
public class ChannelsHub : Hub
{
    private readonly IMediator _mediator;

    public ChannelsHub(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task SendMessage(CreateMessageCommand createMessageCommand)
    {
        var messageDto = await _mediator.Send(createMessageCommand);

        switch (createMessageCommand.ChannelType)
        {
            case ChannelType.Group:
                await SendMessageInGroupAsync(messageDto);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(createMessageCommand.ChannelType));
        }
    }

    private async Task SendMessageInGroupAsync(MessageDto messageDto)
    {
        if (messageDto.ChannelId is not null)
        {
            await Clients.Group(messageDto.ChannelId)
                .SendAsync("Receive", messageDto);
        }
    }
}
