using Microsoft.AspNetCore.Mvc;

using Folks.ChannelsService.Api.Constants;
using Folks.ChannelsService.Application.Features.Channels.Enums;

namespace Folks.ChannelsService.Api.Controllers;

[ApiController]
[Route($"{ApiRoutePatterns.MessagesController}")]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(ILogger<MessagesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public void GetAll(string channelId, ChannelType channelType)
    {
        _logger.LogInformation($"GetAll: channelId = {channelId}, channelType = {channelType}");
    }

    [HttpPut("{messageId}")]
    public void Update(string channelId, string messageId)
    {
        _logger.LogInformation($"Update: messageId = {messageId}");
    }

    [HttpDelete("{messageId}")]
    public void Delete(string channelId, string messageId)
    {
        _logger.LogInformation($"Delete: messageId = {messageId}");
    }
}
