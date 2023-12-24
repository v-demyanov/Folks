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

    public override async Task OnConnectedAsync()
    {
        var currentUserId = Context.UserIdentifier;
        if (currentUserId is not null)
        {
            HubConnectionsStore.AddConnection(currentUserId, Context.ConnectionId);

            var getOwnChannelsQuery = new GetOwnChannelsQuery { OwnerId = currentUserId };
            var channels = await _mediator.Send(getOwnChannelsQuery);

            foreach (var channel in channels)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, channel.Id);
            }
        }

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var currentUserId = Context.UserIdentifier;
        if (currentUserId is not null)
        {
            HubConnectionsStore.RemoveConnection(currentUserId, Context.ConnectionId);
        }

        return base.OnDisconnectedAsync(exception);
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
                .SendAsync("MessageSent", messageDto);
        }
    }
}
