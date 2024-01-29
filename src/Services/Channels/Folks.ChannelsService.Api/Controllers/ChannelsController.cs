// Copyright (c) v-demyanov. All rights reserved.

using System.Security.Claims;

using AutoMapper;

using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;
using Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;
using Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;
using Folks.ChannelsService.Domain.Common.Enums;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.ChannelsController}")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public ChannelsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetOwn()
    {
        var ownerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var channels = await this.mediator.Send(new GetOwnChannelsQuery
        {
            OwnerId = ownerId ?? string.Empty,
        });

        return this.Ok(channels);
    }

    [HttpPost("leave")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<LeaveChannelCommandResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaveChannelCommandResult>>> Leave([FromBody] IEnumerable<LeaveChannelRequest> batch)
    {
        var leaveChannelCommands = this.mapper.Map<IEnumerable<LeaveChannelCommand>>(batch);
        var results = new List<LeaveChannelCommandResult>();
        var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        foreach (var leaveChannelCommand in leaveChannelCommands)
        {
            LeaveChannelCommandResult? result;
            try
            {
                result = await this.mediator.Send(leaveChannelCommand);
                if (result is LeaveChannelCommandSuccessResult)
                {
                    _ = this.HandleLeaveChannelCommandResultEventsAsync((LeaveChannelCommandSuccessResult)result, currentUserId);
                }
            }
            catch (Exception exception)
            {
                result = new LeaveChannelCommandErrorResult
                {
                    ChannelId = leaveChannelCommand.ChannelId,
                    ChannelType = leaveChannelCommand.ChannelType,
                    Error = exception.Message,
                };
            }

            if (result is not null)
            {
                results.Add(result);
            }
        }

        return this.Ok(results);
    }

    private async Task HandleLeaveChannelCommandResultEventsAsync(LeaveChannelCommandSuccessResult result, string currentUserId)
    {
        foreach (var internalEvent in result.Events)
        {
            switch (internalEvent.Key)
            {
                case LeaveChannelCommandInternalEvent.ChannelRemoved:
                    await this.HandleChannelRemovedAsync(result, internalEvent.Value);
                    continue;
                case LeaveChannelCommandInternalEvent.NewGroupOwnerSet:
                    await this.HandleNewGroupOwnerSetAsync(result, internalEvent.Value, currentUserId);
                    continue;
                case LeaveChannelCommandInternalEvent.UserLeftChannel:
                    await this.HandleUserLeftChannelAsync(result, internalEvent.Value, currentUserId);
                    continue;
                default: continue;
            }
        }
    }

    private async Task HandleChannelRemovedAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients)
    {
        var channelDto = await this.mediator.Send(new GetChannelQuery
        {
            Id = result.ChannelId,
            Type = result.ChannelType,
        });

        await this.mediator.Publish(new ChannelRemovedNotification
        {
            ChannelDto = channelDto,
            Recipients = recipients,
        });
    }

    private async Task HandleNewGroupOwnerSetAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients, string currentUserId)
    {
        var messageDto = await this.mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            SentAt = DateTimeOffset.Now,
            Type = MessageType.NewGroupOwnerSetEvent,
        });

        await this.mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = recipients,
        });

        var channelDto = await this.mediator.Send(new GetChannelQuery
        {
            Id = result.ChannelId,
            Type = result.ChannelType,
        });

        await this.mediator.Publish(new ChannelUpdatedNotification
        {
            ChannelDto = channelDto,
            Recipients = recipients,
        });
    }

    private async Task HandleUserLeftChannelAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients, string currentUserId)
    {
        var messageDto = await this.mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            SentAt = DateTimeOffset.Now,
            Type = MessageType.UserLeftEvent,
        });

        await this.mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = recipients,
        });

        var channelDto = await this.mediator.Send(new GetChannelQuery
        {
            Id = result.ChannelId,
            Type = result.ChannelType,
        });

        await this.mediator.Publish(new ChannelUpdatedNotification
        {
            ChannelDto = channelDto,
            Recipients = recipients,
        });
    }
}
