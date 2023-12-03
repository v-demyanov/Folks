using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using Folks.ChatService.Api.Constants;
using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Api.Models;
using Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;

namespace Folks.ChatService.Api.Controllers;

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
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateGroupCommand createGroupCommand)
    {
        var group = await _mediator.Send(createGroupCommand);
        return Ok(group);
    }
}
