using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using MediatR;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;

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

        var userIds = request.ChannelType switch
        {
            ChannelType.Group => _dbContext.Users
                .GetByGroupId(ObjectId.Parse(messageDto.ChannelId))
                .Select(user => user.SourceId),
            _ => new List<string>(),
        };

        await _mediator.Publish(new MessageCreatedNotification 
        { 
            MessageDto = messageDto, 
            Recipients = userIds,
        });
    }
}
