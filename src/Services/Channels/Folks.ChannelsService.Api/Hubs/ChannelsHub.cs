using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using MediatR;

using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Channels.Enums;

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
        switch (createMessageCommand.ChannelType)
        {
            case ChannelType.Group:
                await SendMessageInGroupAsync(createMessageCommand);
                break;
            default:
                break;
        }
    }

    private async Task SendMessageInGroupAsync(CreateMessageCommand createMessageCommand)
    {
        var messageDto = await _mediator.Send(createMessageCommand);
        if (messageDto.ChannelId is not null)
        {
            await Clients.Group(messageDto.ChannelId).SendAsync("Receive", messageDto);
        }
    }
}
