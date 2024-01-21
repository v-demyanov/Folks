using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using System.Security.Claims;

using Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;
using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

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
        var messages = await _mediator.Send(new GetMessagesQuery 
        { 
            ChannelId = channelId, 
            ChannelType = channelType,
        });

        return Ok(messages);
    }

    [HttpPut("readContents")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ReadContents([FromBody] IEnumerable<string> messageIds, string channelId, ChannelType channelType)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _mediator.Send(new ReadMessageContentsCommand
        {
            MessageIds = messageIds,
            ChannelId = channelId,
            ChannelType = channelType,
            UserId = userId ?? string.Empty,
        });

        return NoContent();
    }
}
