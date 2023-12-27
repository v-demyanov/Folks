using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MediatR;

using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;
using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.GroupsController}")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GroupsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateGroupRequest createGroupRequest)
    {
        var createGroupCommand = _mapper.Map<CreateGroupCommand>(createGroupRequest);
        var channelDto = await _mediator.Send(createGroupCommand);

        _ = _mediator.Publish(new ChannelCreatedNotification 
        { 
            ChannelDto = channelDto, 
            Recipients = createGroupRequest.UserIds,
        });

        return Ok(channelDto);
    }
}
