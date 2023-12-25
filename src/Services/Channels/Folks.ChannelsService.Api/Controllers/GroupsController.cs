using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using MediatR;

using Folks.ChannelsService.Api.Constants;
using Folks.ChannelsService.Application.Features.Channels.Dto;
using Folks.ChannelsService.Api.Models;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Api.Hubs;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.GroupsController}")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<ChannelsHub> _channelsHubContext;

    public GroupsController(IMediator mediator, IHubContext<ChannelsHub> channelsHubContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _channelsHubContext = channelsHubContext ?? throw new ArgumentNullException(nameof(channelsHubContext));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateGroupCommand createGroupCommand)
    {
        var group = await _mediator.Send(createGroupCommand);

        foreach (var userId in createGroupCommand.UserIds)
        {
            _ = _channelsHubContext.Clients
                .Group(userId)
                .SendAsync("ChannelCreated", group);
        }

        return Ok(group);
    }
}
