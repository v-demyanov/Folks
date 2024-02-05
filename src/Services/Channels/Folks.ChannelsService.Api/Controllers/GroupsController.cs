// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Common.Contracts;
using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelCreatedNotification;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.GroupsController}")]
public class GroupsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ICurrentUserService currentUserService;

    public GroupsController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this.currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChannelDto>> Create([FromBody] CreateGroupRequest createGroupRequest)
    {
        var createGroupCommand = this.mapper.Map<CreateGroupCommand>(createGroupRequest);
        var channelDto = await this.mediator.Send(createGroupCommand);

        _ = this.mediator.Publish(new ChannelCreatedNotification
        {
            ChannelDto = channelDto,
            Recipients = createGroupRequest.UserIds,
        });

        return this.Ok(channelDto);
    }

    [HttpGet("{groupId}")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GroupDto>> Get(string groupId)
    {
        var currentUserId = this.currentUserService.GetUserId();
        var groupDto = await this.mediator.Send(new GetGroupQuery
        {
            CurrentUserId = currentUserId ?? string.Empty,
            GroupId = groupId,
        });

        return this.Ok(groupDto);
    }
}
