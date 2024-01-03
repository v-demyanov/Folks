using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using System.Security.Claims;

using Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;
using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;
using Folks.ChannelsService.Domain.Enums;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.ChannelsController}")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChannelsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetOwn()
    {
        var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var channels = await _mediator.Send(new GetOwnChannelsQuery 
        { 
            OwnerId = ownerId ?? string.Empty,
        });

        return Ok(channels);
    }

    [HttpPost("leave")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<LeaveChannelCommandResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaveChannelCommandResult>>> Leave([FromBody] IEnumerable<LeaveChannelRequest> batch)
    {
        var leaveChannelCommands = _mapper.Map<IEnumerable<LeaveChannelCommand>>(batch);
        var results = new List<LeaveChannelCommandResult>();
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        foreach (var leaveChannelCommand in leaveChannelCommands)
        {
            LeaveChannelCommandResult? result;
            try
            {
                result = await _mediator.Send(leaveChannelCommand);
                if (result is LeaveChannelCommandSuccessResult)
                {
                    _ = HandleLeaveChannelCommandResultEventsAsync((LeaveChannelCommandSuccessResult)result, currentUserId);
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

        return Ok(results);
    }

    private async Task HandleLeaveChannelCommandResultEventsAsync(LeaveChannelCommandSuccessResult result, string currentUserId)
    {
        foreach (var internalEvent in result.Events)
        {
            switch (internalEvent.Key)
            {
                case LeaveChannelCommandInternalEvent.ChannelRemoved:
                    await HandleChannelRemovedAsync(result, internalEvent.Value);
                    continue;
                case LeaveChannelCommandInternalEvent.NewGroupOwnerSet:
                    await HandleNewGroupOwnerSetAsync(result, internalEvent.Value, currentUserId);
                    continue;
                case LeaveChannelCommandInternalEvent.UserLeftChannel:
                    await HandleUserLeftChannelAsync(result, internalEvent.Value, currentUserId);
                    continue;
                default: continue;
            }
        }
    }

    private async Task HandleChannelRemovedAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients) =>
        await _mediator.Publish(new ChannelRemovedNotification
        {
            ChannelDto = new ChannelDto
            { 
                Id = result.ChannelId, 
                Type = result.ChannelType, 
                Title = result.ChannelTitle ?? string.Empty,
            },
            Recipients = recipients,
        });

    private async Task HandleNewGroupOwnerSetAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients, string currentUserId)
    {
        var messageDto = await _mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            SentAt = DateTimeOffset.Now,
            Type = MessageType.NewGroupOwnerSetEvent,
        });

        await _mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = recipients,
        });
    }

    private async Task HandleUserLeftChannelAsync(LeaveChannelCommandSuccessResult result, IEnumerable<string> recipients, string currentUserId)
    {
        var messageDto = await _mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            SentAt = DateTimeOffset.Now,
            Type = MessageType.UserLeftEvent,
        });

        await _mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = recipients,
        });
    }
}
