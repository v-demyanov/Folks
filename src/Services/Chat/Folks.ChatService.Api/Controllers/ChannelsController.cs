using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using System.Security.Claims;
using System.Net;

using Folks.ChatService.Api.Constants;
using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

namespace Folks.ChatService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.BaseRoute}[controller]")]
public class ChannelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<ChannelDto>), (int)HttpStatusCode.OK)]
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
