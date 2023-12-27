using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using System.Security.Claims;

using Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelRemovedNotification;
using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessageCreatedNotification;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.ChannelsController}")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ChannelsServiceDbContext _dbContext;

    public ChannelsController(IMediator mediator, IMapper mapper, ChannelsServiceDbContext dbContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetOwnChannels()
    {
        var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var channels = await _mediator.Send(new GetOwnChannelsQuery 
        { 
            OwnerId = ownerId ?? string.Empty,
        });

        return Ok(channels);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<LeaveChannelCommandResult>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaveChannelCommandResult>>> LeaveChannels([FromBody] IEnumerable<LeaveChannelRequest> batch)
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
        foreach (var channelsHubEvent in result.Events)
        {
            switch (channelsHubEvent)
            {
                case LeaveChannelCommandInternalEvent.ChannelRemoved:
                    await HandleChannelRemovedAsync(result);
                    continue;
                case LeaveChannelCommandInternalEvent.NewGroupOwnerSet:
                    await HandleNewGroupOwnerSetAsync(result, currentUserId);
                    continue;
                case LeaveChannelCommandInternalEvent.UserLeft:
                    await HandleUserLeftAsync(result, currentUserId);
                    continue;
                default: continue;
            }
        }
    }

    private async Task HandleChannelRemovedAsync(LeaveChannelCommandSuccessResult result) =>
        await _mediator.Publish(new ChannelRemovedNotification
        {
            ChannelDto = new ChannelDto
            { 
                Id = result.ChannelId, 
                Type = result.ChannelType, 
                Title = result.ChannelTitle ?? string.Empty,
            },
            Recipients = result.Recipients,
        });

    private async Task HandleNewGroupOwnerSetAsync(LeaveChannelCommandSuccessResult result, string currentUserId)
    {
        var messageDto = await _mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            Content = "New owner has been set",
            SentAt = DateTimeOffset.Now,
            IsSpecific = true,
        });

        await _mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = result.Recipients,
        });
    }

    private async Task HandleUserLeftAsync(LeaveChannelCommandSuccessResult result, string currentUserId)
    {
        var currentUser = _dbContext.Users.GetBySourceId(currentUserId);
        var messageDto = await _mediator.Send(new CreateMessageCommand
        {
            OwnerId = currentUserId,
            ChannelId = result.ChannelId,
            ChannelType = result.ChannelType,
            Content = $"{currentUser.UserName} has left",
            SentAt = DateTimeOffset.Now,
            IsSpecific = true,
        });

        await _mediator.Publish(new MessageCreatedNotification
        {
            MessageDto = messageDto,
            Recipients = result.Recipients,
        });
    }
}
