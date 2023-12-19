using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using Folks.ChannelsService.Api.Constants;
using Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;
using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Api.Models;
using Folks.ChannelsService.Application.Features.Channels.Enums;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.MessagesController}")]
public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<MessageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MessageDto>>> Get(string channelId, ChannelType channelType)
    {
        var getMessagesQuery = new GetMessagesQuery { ChannelId = channelId, ChannelType = channelType };
        var messages = await _mediator.Send(getMessagesQuery);
        return Ok(messages);
    }
}
