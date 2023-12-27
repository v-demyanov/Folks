using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;
using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.GroupsController}")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateGroupCommand createGroupCommand)
    {
        var channelDto = await _mediator.Send(createGroupCommand);

        _ = _mediator.Publish(new ChannelCreatedNotification 
        { 
            ChannelDto = channelDto, 
            Recipients = createGroupCommand.UserIds,
        });

        return Ok(channelDto);
    }
}
