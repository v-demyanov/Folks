using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using System.Security.Claims;

using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Application.Features.Channels.Queries.GetOwnChannelsQuery;
using Folks.ChatService.Api.Constants;
using Folks.ChatService.Api.Models;

namespace Folks.ChatService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.ChannelsController}")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetOwnChannels()
    {
        var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (ownerId is null)
        {
            return Unauthorized();
        }

        var query = new GetOwnChannelsQuery { OwnerId = ownerId };
        var channels = await _mediator.Send(query);

        return Ok(channels);
    }
}
