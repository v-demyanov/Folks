using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Folks.ChatService.Api.Hubs;

[Authorize]
public class ChannelsHub : Hub
{
    public void EnterGroup()
    {
    }

    public void SendChatMessage()
    {
        // Create new chat if it doesn't exist
    }

    public void SendGroupMessage()
    {
    }
}
