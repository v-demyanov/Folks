using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using MediatR;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Channels.Enums;
using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Api.Models;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Api.Hubs;

[Authorize]
public class ChannelsHub : Hub
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ChatServiceDbContext _dbContext;

    public ChannelsHub(IMediator mediator, IMapper mapper, ChatServiceDbContext dbContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public override async Task OnConnectedAsync()
    {
        var currentUserId = Context.UserIdentifier;
        if (currentUserId is not null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, currentUserId);
        }

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        var createMessageCommand = _mapper.Map<CreateMessageCommand>(request);
        var messageDto = await _mediator.Send(createMessageCommand);

        switch (request.ChannelType)
        {
            case ChannelType.Group:
                await SendMessageInGroupAsync(messageDto);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request.ChannelType));
        }
    }

    private async Task SendMessageInGroupAsync(MessageDto messageDto)
    {
        if (messageDto.ChannelId is null)
        {
            return;
        }

        var users = _dbContext.Users.GetByGroupId(ObjectId.Parse(messageDto.ChannelId));
        foreach (var user in users)
        {
            await Clients.Group(user.SourceId)
                .SendAsync("MessageSent", messageDto);
        }
    }
}
