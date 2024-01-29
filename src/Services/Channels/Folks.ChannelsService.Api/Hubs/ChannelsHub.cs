// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;
using Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;

namespace Folks.ChannelsService.Api.Hubs;

[Authorize]
public class ChannelsHub : Hub
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ChannelsServiceDbContext dbContext;

    public ChannelsHub(IMediator mediator, IMapper mapper, ChannelsServiceDbContext dbContext)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public override async Task OnConnectedAsync()
    {
        var currentUserId = this.Context.UserIdentifier;
        if (currentUserId is not null)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, currentUserId);
        }

        await base.OnConnectedAsync();
    }

    public async Task SendMessage(SendMessageRequest request)
    {
        var createMessageCommand = this.mapper.Map<CreateMessageCommand>(request);
        var messageDto = await this.mediator.Send(createMessageCommand);

        var userIds = request.ChannelType switch
        {
            ChannelType.Group => this.dbContext.Users
                .GetByGroupId(ObjectId.Parse(messageDto.ChannelId))
                .Select(user => user.SourceId),
            _ => new List<string>(),
        };

        _ = this.mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = userIds,
        });

        var channelDto = await this.mediator.Send(new GetChannelQuery
        {
            Id = request.ChannelId,
            Type = request.ChannelType,
        });

        _ = this.mediator.Publish(new ChannelUpdatedNotification
        {
            ChannelDto = channelDto,
            Recipients = userIds,
        });
    }
}
