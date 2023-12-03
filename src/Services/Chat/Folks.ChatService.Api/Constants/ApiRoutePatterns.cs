namespace Folks.ChatService.Api.Constants;

public static class ApiRoutePatterns
{
    public const string BaseRoute = "api/v1/";

    public const string ChannelsController = $"{BaseRoute}channels";

    public const string GroupsController = $"{ChannelsController}/groups";

    public const string ChannelsHub = $"{BaseRoute}hubs/channels";

}
