// Copyright (c) v-demyanov. All rights reserved.

using System.Security.Claims;

using Folks.ChannelsService.Api.Common.Constants;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Channels.Notifications.ChannelUpdatedNotitfication;
using Folks.ChannelsService.Application.Features.Channels.Queries.GetChannelQuery;
using Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Notifications.MessagesUpdatedNotification;
using Folks.ChannelsService.Application.Features.Messages.Queries.GetMessagesQuery;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Folks.ChannelsService.Api.Controllers;

[Authorize]
[ApiController]
[Route($"{ApiRoutePatterns.MessagesController}")]
public class MessagesController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ChannelsServiceDbContext dbContext;

    public MessagesController(IMediator mediator, ChannelsServiceDbContext dbContext)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(IEnumerable<MessageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MessageDto>>> Get(string channelId, ChannelType channelType)
    {
        var messages = await this.mediator.Send(new GetMessagesByChannelQuery
        {
            ChannelId = channelId,
            ChannelType = channelType,
        });

        return this.Ok(messages);
    }

    [HttpPut("readContents")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ReadContents([FromBody] IEnumerable<string> messageIds, string channelId, ChannelType channelType)
    {
        var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        await this.mediator.Send(new ReadMessageContentsCommand
        {
            MessageIds = messageIds,
            ChannelId = channelId,
            ChannelType = channelType,
            UserId = userId ?? string.Empty,
        });

        _ = this.FireReadContentsSuccessEventsAsync(channelId, channelType, messageIds);

        return this.NoContent();
    }

    private Task FireReadContentsSuccessEventsAsync(string channelId, ChannelType channelType, IEnumerable<string> messageIds)
    {
        var recipients = channelType switch
        {
            ChannelType.Chat => this.dbContext.Users.GetByChatId(channelId).Select(x => x.SourceId),
            ChannelType.Group => this.dbContext.Users.GetByGroupId(channelId).Select(x => x.SourceId),
            _ => new List<string>(),
        };

        _ = this.FireChannelUpdatedEventAsync(channelId, channelType, recipients);
        _ = this.FireMessagesUpdatedEventAsync(messageIds, recipients);

        return Task.CompletedTask;
    }

    private async Task FireChannelUpdatedEventAsync(string channelId, ChannelType channelType, IEnumerable<string> recipients)
    {
        var channelDto = await this.mediator.Send(new GetChannelQuery
        {
            Id = channelId,
            Type = channelType,
        });

        await this.mediator.Publish(new ChannelUpdatedNotification
        {
            ChannelDto = channelDto,
            Recipients = recipients,
        });
    }

    private async Task FireMessagesUpdatedEventAsync(IEnumerable<string> messageIds, IEnumerable<string> recipients)
    {
        var messagesDto = await this.mediator.Send(new GetMessagesQuery
        {
            MessageIds = messageIds,
        });

        await this.mediator.Publish(new MessagesUpdatedNotification
        {
            Messages = messagesDto,
            Recipients = recipients,
        });
    }
}
